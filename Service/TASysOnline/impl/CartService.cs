using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class CartService : ICartService
    {
        private ICartRepository _cartRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private ICourseRepository _courseRepository;

        private IUserAccountRepository _userAccountRepository;

        public CartService(ICartRepository cartRepository, 
                            IUriService uriService, 
                            IMapper mapper, 
                            ICourseRepository courseRepository,
                            IUserAccountRepository userAccountRepository)
        {
            this._cartRepository = cartRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._courseRepository = courseRepository;
            this._userAccountRepository = userAccountRepository;
        }

        public async Task<Response> AddCourseToCart(Guid userId, Guid courseId)
        {
            var user = await this._userAccountRepository.GetUserAccountEagerLoadCourse(userId);

            if (user == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var course = await this._courseRepository.FindByIdAsyncEagerLoad(courseId);
            if (course == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Course not found!" };
            }

            var courseOfLearnerIds = user.CoursesOfLearner.Select(s => s.Id).ToList();

            if (courseOfLearnerIds.Contains(courseId))
            {
                return new Response { StatusCode = StatusCodes.Status400BadRequest, ResponseMessage = "Course was bought!" };
            }

            var cart = await this._cartRepository.GetCartByUserIdAsync(userId);

            var courseInCartIds = cart.Courses.Select(s => s.Id).ToList();

            if (courseInCartIds.Contains(courseId))
            {
                return new Response { StatusCode = StatusCodes.Status400BadRequest, ResponseMessage = "Course already in cart!" };
            }

            if (course.AvailableSlot >= course.MaxSlot)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Course is out of slot!" };
            }

            course.AvailableSlot += 1;

            await this._courseRepository.UpdateAsync(course);
            await this._courseRepository.SaveAsync();

            await this._cartRepository.AddCourseToCart(course, cart.Id);

            cart.TotalCost += course.Cost;
            cart.TotalCourse += 1;

            await this._cartRepository.UpdateAsync(cart);
            await this._cartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Add course to cart successfully!" };
        }

        public async Task<Response> CreateCartAsync(CartRequest CartRequest)
        {

            var table = this._mapper.Map<CartTable>(CartRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._cartRepository.InsertAsync(table);
            await this._cartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Cart was created!" };
        }

        public async Task<Response> DeleteAllCart()
        {
            await this._cartRepository.DeleteAllAsyn();
            await this._cartRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Cart successfully!"
            };
        }

        public async Task<Response> DeleteCart(Guid[] CartId)
        {
            for (var i = 0; i < CartId.Length; i++)
            {
                await this._cartRepository.DeleteAsync(CartId[i]);
            }

            await this._cartRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Cart successfully!"
            };
        }

        public async Task<FilterResponse<List<CartResponse>>> FilterCartBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._cartRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CartResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._cartRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CartResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<CartResponse>> GetAllCartAsync()
        {
            var tables = await this._cartRepository.GetAllAsync();

            var responses = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<CartResponse>>> GetAllCartPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._cartRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._cartRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CartResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CartResponse> GetCartByUserId(Guid userId, AccountAuthorInfo accountAuthorInfo)
        {
            var table = await this._cartRepository.GetCartByUserIdAsync(userId);

            if (table == null)
            {
                return new CartResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            if (table.UserAccountId != accountAuthorInfo.Id && accountAuthorInfo.Role != Roles.Admin)
            {
                return new CartResponse { StatusCode = StatusCodes.Status403Forbidden, ResponseMessage = "Invalid access data!" };
            }

            var response = this._mapper.Map<CartResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Cart successfully";
            return response;
        }

        public async Task<SearchResponse<List<CartResponse>>> SearchCartBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._cartRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CartResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._cartRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CartResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateCart(CartRequest cartRequest, AccountAuthorInfo accountAuthorInfo)
        {
            var table = await this._cartRepository.FindByIdAsync(cartRequest.Id);

            if (table == null)
            {
                return new CartResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            if (table.UserAccountId != accountAuthorInfo.Id && accountAuthorInfo.Role != Roles.Admin)
            {
                return new CartResponse { StatusCode = StatusCodes.Status403Forbidden, ResponseMessage = "Invalid access data!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.TotalCourse = cartRequest.TotalCourse;

            await this._cartRepository.UpdateAsync(table);
            await this._cartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Cart successfully!" };
        }

        public async Task<Response> RemoveCourseFromCart(Guid userId, Guid courseId)
        {
            var course = await this._courseRepository.FindByIdAsyncEagerLoad(courseId);
            if (course == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Course not found!" };
            }

            var cart = await this._cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            cart.TotalCourse -= 1;
            cart.TotalCost -= course.Cost;
            await this._cartRepository.RemoveCourseFromCart(courseId, cart.Id);
            await this._cartRepository.UpdateAsync(cart);
            await this._cartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Remove course from cart successfully!" };
        }

        public async Task<Response> RemoveAllCourseFromCart(Guid userId)
        {
            var cart = await this._cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var coursesOfCart = cart.Courses.ToList();
            foreach(var course in coursesOfCart)
            {
                await this._cartRepository.RemoveCourseFromCart(course.Id, cart.Id);
            }
            cart.TotalCourse = 0;
            cart.TotalCost = 0;
            await this._cartRepository.UpdateAsync(cart);
            await this._cartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Remove all course from cart successfully!" };
        }
    }
}
