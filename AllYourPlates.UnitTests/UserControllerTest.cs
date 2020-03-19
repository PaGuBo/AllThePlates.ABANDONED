using AllYourPlates.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using AllYourPlates.Domain.Abstract;
using System.Web.Mvc;
using Moq;
using AllYourPlates.Mock;
using System.Linq;
using System.Collections.Generic;
using AllYourPlates.Domain.Entities;
using AllYourPlates.WebUI.Models;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;

namespace AllYourPlates.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for DayControllerTest and is intended
    ///to contain all DayControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserControllerTest
    {


        private TestContext testContextInstance;
        private Mock<IRepository> mock;
        private UserController controller;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestInitialize]
        public void Setup()
        {
            /*mock = new Mock<IRepository>();
            mock.Setup(d => d.Users).Returns(MockData.MemoryList.AsQueryable());
            controller = new UserController(mock.Object);
            controller.PageSize = 2;*/
        }

        [TestMethod]
        public void TestAutoMapping()
        {
            // arrange

            ISessionFactory factory = CreateSessionFactory();

            // action
            

            using (var session = factory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    User u = new User { 
                        BodyShots = new List<BodyShot>(),
                        Name = "Pavel Gutin",
                        Plates = new List<Plate>(),
                        Workouts = new List<Workout>()
                    };
                    session.Save(u);
                    transaction.Commit();
                }
            }
            // assert
            using (var session = factory.OpenSession())
            {
                var users = session.CreateCriteria<User>().List<User>();
                var u = users.FirstOrDefault<User>();
                Assert.AreEqual(u.Name, "Pavel Gutin");
            }
            

        }


        private static ISessionFactory CreateSessionFactory()
        {

            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                          .UsingFile("firstProject.db"))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<User>()))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // delete the existing db on each run
            if (File.Exists("firstProject.db"))
                File.Delete("firstProject.db");

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
              .Create(false, true);
        }
        /// <summary>
        ///A test for List
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        ////[TestMethod]
        ////public void Can_Paginate()
        ////{
        ////    //action
        ////    UsersListViewModel result = (UsersListViewModel)controller.List(null, 2).Model;

        ////    //assert

        ////    User[] dayArray = result.Users.ToArray();
        ////    Assert.IsTrue(dayArray.Length == 2);

        ////    Assert.AreEqual(dayArray[0].Name, "Homer");
        ////    Assert.IsNotNull(dayArray[0].Plates.Find(p => p.Title == "Doughnut"));
        ////    Assert.IsNotNull(dayArray[0].Plates.Find(p => p.Title == "Porkchops"));
        ////    Assert.IsNotNull(dayArray[0].Plates.Find(p => p.Title == "Slushie"));
            
        ////    Assert.AreEqual(dayArray[1].Name, "Pavel");
        ////    Assert.IsNotNull(dayArray[1].Plates.Find(p => p.Title == "Omelette"));
        ////    Assert.IsNotNull(dayArray[1].Plates.Find(p => p.Title == "Panera sandwich"));
        ////    Assert.IsNotNull(dayArray[1].Plates.Find(p => p.Title == "Sushi"));


        ////}

        /// <summary>
        ///A test for List
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        ////[TestMethod()]
        ////public void Can_Send_Pagination_View_Model()
        ////{
        ////    //action
        ////    UsersListViewModel result = (UsersListViewModel)controller.List(null, 2).Model;

        ////    //assert
        ////    PagingInfo pageInfo = result.PagingInfo;
        ////    Assert.AreEqual(pageInfo.CurrentPage, 2);
        ////    Assert.AreEqual(pageInfo.ItemsPerPage, 2);
        ////    Assert.AreEqual(pageInfo.TotalItems, 4);
        ////    Assert.AreEqual(pageInfo.TotalPages, 2);
        ////}
        ////[TestMethod]
        ////public void Can_Filter_Users()
        ////{
        ////    User[] result = ((UsersListViewModel)controller.List("Pavel", 1).Model).Users.ToArray();

        ////    //assert
        ////    Assert.AreEqual(result.Length, 1);
        ////    Assert.AreEqual(result[0].Name, "Pavel");
        ////}
    }
}
