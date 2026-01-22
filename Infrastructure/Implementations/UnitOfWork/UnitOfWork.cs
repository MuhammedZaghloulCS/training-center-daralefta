using Azure;
using Infrastructure.Abstractions.IRepositories;
using Infrastructure.Abstractions.IUnitOfWork;
using Infrastructure.Context;
using Infrastructure.Implementations.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Implementations.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        #region Fields
        private BuildingRepository buildingRepository;
        private RoomRepository roomRepository;
        private SessionRepository sessionRepository;
        private CourseRepository courseRepository;
        private TrainingRepository trainingRepository;
        private SurveyRepository surveyRepository;
        private SurveyCategoryRepository surveyCategoryRepository;
        private SurveyQuestionRepository surveyQuestionRepository;
        private SurveyAnswerRepository surveyAnswerRepository;
        private SurveyResponseRepository surveyResponseRepository;
        private readonly ApplicationContext context;
        #endregion
        //CTOR
        public UnitOfWork(ApplicationContext context)
        {
            this.context = context;
        }

        public IBuildingRepository IBuildings {
            get
            {

                if (buildingRepository == null)
                {
                    buildingRepository = new BuildingRepository(context);
                }
                return buildingRepository;
            }
        }

        public IRoomRepository IRooms {
            get { 
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(context);
                }
                return roomRepository;
            }
        }

        public ISessionRepository ISession
        {
            get
            {
                if(sessionRepository == null)
                 
                    sessionRepository = new SessionRepository(context);
                   
                return sessionRepository;
            }
        }

        public ICourseRepository ICourse
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository(context);
                }
                return courseRepository;

            }
        }

        public ITrainingRepository ITraining
        {
            get
            {
                if (trainingRepository == null)
                {
                    trainingRepository = new TrainingRepository(context);
                }
                return trainingRepository;

            }
        }

        public ISessionRepository IServey
        {
            get
            {
                if (sessionRepository==null)
                {
                    sessionRepository = new SessionRepository(context);
                }
                return sessionRepository;
            }
        }

        public ISurveyCategoryRepository ISurveyCategory
        {
            get
            {
                if (surveyCategoryRepository == null)
                {
                    surveyCategoryRepository = new SurveyCategoryRepository(context);
                }
                return surveyCategoryRepository;
            }
        }

        public ISurveyQuestionRepository ISurveyQuestion
        {
            get { 
                if (surveyQuestionRepository == null)
                {
                    surveyQuestionRepository = new SurveyQuestionRepository(context);
                }
                return surveyQuestionRepository;
            }
        }

        public ISurveyAnswerRepository ISurveyAnswer
        {
            get
            {
                if (surveyAnswerRepository == null)
                {
                    surveyAnswerRepository = new SurveyAnswerRepository(context);
                }
                return surveyAnswerRepository;

            }
        }
        public ISurveyResponseRepository ISurveyResponse    
        {
            get
            {
                if (surveyResponseRepository == null)
                {
                    surveyResponseRepository = new SurveyResponseRepository(context);
                }
                return surveyResponseRepository;

            }
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();

        }
        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
