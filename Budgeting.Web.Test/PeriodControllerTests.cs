using Budgeting.Core.Exceptions;
using Budgeting.Entity;
using Budgeting.Web.Controllers;
using Budgeting.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Budgeting.Web.Test
{
    [TestClass]
    public class PeriodControllerTests
    {
        private Mock<ModelContext> _mockContext;

        private void InitialiseData()
        {
            var data = new List<Period>
            {
                new Period { PeriodId = 1, Title  = "First Period", StartDate = DateTime.Parse("01 July 2016"), StartingBalance = 100, UserAccountId = 1, CreatedDate = DateTime.Parse("01 June 2016") },
                new Period { PeriodId = 2, Title  = "Second Period", StartDate = DateTime.Parse("01 August 2016"), StartingBalance = 200, UserAccountId = 1, CreatedDate = DateTime.Parse("01 May 2016") },
                new Period { PeriodId = 3, Title  = "Third Period", StartDate = DateTime.Parse("01 September 2016"), StartingBalance = 200, UserAccountId = 1, CreatedDate = DateTime.Parse("01 April 2016"), ArchivedDate = DateTime.Parse("02 April 2016") },
                new Period { PeriodId = 4, Title  = "Other Account Period", StartDate = DateTime.Parse("01 August 2016"), StartingBalance = 200, UserAccountId = 2, CreatedDate = DateTime.Parse("01 May 2016") }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Period>>();
            mockSet.As<IQueryable<Period>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Period>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Period>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Period>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext = new Mock<ModelContext>();
            _mockContext.Setup(c => c.Periods).Returns(mockSet.Object);
        }

        [TestMethod]
        public void PeriodController_GetbyID_Test()
        {
            InitialiseData();

            //TODO: Finish off fake code to test
            var controller = new PeriodController(_mockContext.Object);
            var period = controller.GetbyID(1);
        }
    }
}
