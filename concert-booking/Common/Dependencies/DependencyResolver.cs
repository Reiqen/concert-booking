using concert_booking.BLL;
using concert_booking.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.Common.Dependencies
{
    public class DependencyResolver
    {
        private static IConcertDAO _concertDAO;
        private static IConcertDAO _ConcertDAO => _concertDAO ?? (_concertDAO = new ConcertDAO());

        private static IConcertBL _concertBL;
        public static IConcertBL ConcertBL => _concertBL ?? (_concertBL = new ConcertBL(_ConcertDAO));

        private static IMessageDAO _messageDAO;
        private static IMessageDAO _MessageDAO => _messageDAO ?? (_messageDAO = new MessageDAO());

        private static IMessageBL _messageBL;
        public static IMessageBL MessageBL => _messageBL ?? (_messageBL = new MessageBL(_MessageDAO, _UserDAO));

        private static IFeedbackDAO _feedbackDAO;
        private static IFeedbackDAO _FeedbackDAO => _feedbackDAO ?? (_feedbackDAO = new FeedbackDAO());

        private static IFeedbackBL _feedbackBL;
        public static IFeedbackBL FeedbackBL => _feedbackBL ?? (_feedbackBL = new FeedbackBL(_FeedbackDAO, _ConcertDAO, _UserDAO));

        private static IUserDAO _userDAO;
        private static IUserDAO _UserDAO => _userDAO ?? (_userDAO = new UserDAO());

        private static IUserBL _userBL;
        public static IUserBL UserBL => _userBL ?? (_userBL = new UserBL(_UserDAO, _RoleDAO));

        private static IRoleDAO _roleDAO;
        private static IRoleDAO _RoleDAO => _roleDAO ?? (_roleDAO = new RoleDAO());

        private static IRoleBL _roleBL;
        public static IRoleBL RoleBL => _roleBL ?? (_roleBL = new RoleBL(_RoleDAO));

        private static IImageDAO _imageDAO;
        private static IImageDAO _ImageDAO => _imageDAO ?? (_imageDAO = new ImageDAO());

        private static IImageBL _imageBL;
        public static IImageBL ImageBL => _imageBL ?? (_imageBL = new ImageBL(_ImageDAO, _ConcertDAO));
    }
}