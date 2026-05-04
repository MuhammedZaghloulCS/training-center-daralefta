using Infrastructure.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region properties
        IBuildingRepository IBuildings { get; }
        IRoomRepository IRooms { get; }
        ISessionRepository ISession { get; }
        ICourseRepository ICourse { get; }
        ITrainingRepository ITraining { get; }
        ISurveyRepository ISurvey { get; }
        ISessionRepository IServey { get; }
        ISurveyCategoryRepository ISurveyCategory { get; }
        ISurveyQuestionRepository ISurveyQuestion { get; }
        ISurveyAnswerRepository ISurveyAnswer { get; }
        ISurveyResponseRepository ISurveyResponse { get; }
        IAssignUserTrainingRepository IAssignUserTraining { get; }
        IAssignUserCourseRepository IAssignUserCourse { get; }
        IAssignUserSessionRepository IAssignUserSession { get; }
        IUserSessionRepository IUserSessionRepository { get; }
        IUserCourseRepository IUserCourseRepository { get; }
        IUserTrainingRepository IUserTrainingRepository { get; }
        ITrainingsSurveysRepository ITrainingsSurveys { get; }
        IQuestionAnswerRepository IQuestionAnswer { get; }
        IQuestionRepository IQuestion { get; }
        IAttendanceRepository IAttendance { get; }

        #endregion
        Task<int> Complete();
        public new void Dispose();
    }
}
