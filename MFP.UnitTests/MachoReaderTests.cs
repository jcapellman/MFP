using System;
using System.IO;
using System.Linq;

using MFP.library;
using MFP.library.Enums;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MFP.UnitTests
{
    [TestClass]
    public class MachoReaderTests
    {
        private string GetFullUnitTestPath(string sample) => Path.Combine(AppContext.BaseDirectory, "Samples", sample);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MachoReader_NullFileName()
        {
            var _ = MachoReader.Read(fileName: null);
        }

        [TestMethod]
        public void MachoReader_InvalidEmptyFile()
        {
            var result = MachoReader.Read(fileName: GetFullUnitTestPath("EmptyFile"));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MachoReader_ValidAMD64()
        {
            var result = MachoReader.Read(fileName: GetFullUnitTestPath("Macho_AMD64"));

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Count(a => a.Format == MachoFormat.AMD64) == 1);
        }
    }
}