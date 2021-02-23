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
        private static string GetFullUnitTestPath(string sample) => Path.Combine(AppContext.BaseDirectory, "Samples", sample);

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
        public void MachoReader_ValidARM64()
        {
            var result = MachoReader.Read(fileName: GetFullUnitTestPath("Macho_ARM64"));

            Assert.IsNotNull(result);
            
            Assert.IsTrue(result.Count(a => a.Format == MachoFormat.ARM64) == 1);

            var armResult = result.FirstOrDefault();

            Assert.IsNotNull(armResult);

            Assert.IsTrue(armResult.FileType == FileTypes.Executable);
        }

        [TestMethod]
        public void MachoReader_ValidMulti()
        {
            var result = MachoReader.Read(fileName: GetFullUnitTestPath("Macho_MULTI"));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);
            Assert.IsFalse(result.Any(a => a.Format == MachoFormat.I386));
            Assert.IsTrue(result.Any(a => a.Format == MachoFormat.AMD64));
            Assert.IsTrue(result.Any(a => a.Format == MachoFormat.ARM64));
        }
    }
}