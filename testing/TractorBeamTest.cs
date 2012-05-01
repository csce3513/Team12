using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCow;
using Microsoft.Xna.Framework;

namespace ProjectCowTest
{
    [TestClass]
    public class TractorBeamTest
    {
        [TestMethod]
        public void UpdateMethodTest()
        {
            TractorBeam beam = new TractorBeam(Vector2.Zero);
            beam.Update(new Vector2(100, 200));

            Assert.AreEqual(100, beam.Position.X);
            Assert.AreEqual(200, beam.Position.Y);
        }
    }
}
