using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ufogame;

namespace TestProject1
{
    [TestClass]
    public class ufogametest
    {
        [TestMethod]
        public void InteractObjectPosValid() //test method checks for valid x and y coordinates for the object
        {
            InteractObject objectone = new InteractObject();
            Assert.IsNotNull(objectone.position);
        }

        [TestMethod]
        public void InteractObjectImageValid()
        {
            InteractObject objectone = new InteractObject();
            Assert.IsNotNull(objectone.IAOImage);
        }

        [TestMethod]
        public void InteractObjectSizeValid()
        {
            InteractObject objectone = new InteractObject();
            Assert.IsNotNull(objectone.IAOSize);

        }
        
        /* Method not working
        [TestMethod]
        public void LiftObjectLiftCheck()
        {
            LiftObject liftone;
            liftone = new LiftObject();
            KeyboardState testkey;
            liftone.LiftIAO(testkey);

        }
        */

 
    }
}
