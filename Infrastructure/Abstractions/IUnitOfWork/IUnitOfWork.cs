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


        #endregion
        Task<int> Complete();
        public new void Dispose();
    }
}
