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

            if (course.AvailableSlot <= 0)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Course is out of slot!" };
            }

            course.AvailableSlot -= 1;

            await this._cartRepository.AddCourseToCart(course, cart.Id);

            await this._courseRepository.UpdateAsync(course);
            await this._courseRepository.SaveAsync();

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

        public async Task<CartResponse> GetCartByUserId(Guid userId)
        {
            var table = await this._cartRepository.GetCartByUserIdAsync(userId);

            if (table == null)
            {
                return new CartResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var response = this._mapper.Map<CartResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Cart successfully";
            return response;
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

            course.AvailableSlot += 1;
            await this._courseRepository.UpdateAsync(course);
            await this._courseRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Remove course from cart successfully!" };
        }

        public async Task<Response> RemoveAllCourseFromCart(Guid userId, bool isCreateBill = false)
        {
            var cart = await this._cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var coursesOfCart = cart.Courses.ToList();
            foreach(var course in coursesOfCart)
            {
                if (!isCreateBill) {
                    course.AvailableSlot += 1;
                    await this._courseRepository.UpdateAsync(course);
                    await this._courseRepository.SaveAsync();
                }

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
