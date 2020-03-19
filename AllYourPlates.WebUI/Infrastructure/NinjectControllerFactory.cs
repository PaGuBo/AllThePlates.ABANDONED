using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Web.Routing;
using AllYourPlates.Domain.Abstract;
using Moq;
using AllYourPlates.Domain.Entities;
using AllYourPlates.Mock;

namespace AllYourPlates.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        private string absolutePath;
        public NinjectControllerFactory(string absolutePath)
        {
            ninjectKernel = new StandardKernel();
            this.absolutePath = absolutePath;
            AddBindings(absolutePath);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings(string absolutePath)
        {
            Mock<IRepository> mock = new Mock<IRepository>();

            MockData md = new MockData(absolutePath);

            mock.Setup(m => m.Users).Returns(md.DiskList.AsQueryable());

            ninjectKernel.Bind<IRepository>().ToConstant(mock.Object);
        }
    }
}