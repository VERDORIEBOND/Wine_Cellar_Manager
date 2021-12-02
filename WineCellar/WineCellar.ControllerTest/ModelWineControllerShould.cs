using Controller;
using Microsoft.Extensions.Configuration;
using WineCellar.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineCellar.ControllerTest.Utilities;

namespace WineCellar.ControllerTest
{
    [TestFixture]
    public class ModelWineControllerShould
    {
        [Test, Order(1)]
        public async Task WineController_GetAllWinesShould()
        {
            //Data.GetAlWines unit test
        }
    }
}
