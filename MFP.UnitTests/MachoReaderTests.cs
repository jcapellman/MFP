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
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MachoReader_NullFileName()
        {
            var _ = MachoReader.Read(fileName: null);
        }

        [TestMethod]
        public void MachoReader_ValidAMD64()
        {
            var result = MachoReader.Read(fileName: Path.Combine(AppContext.BaseDirectory, "Samples", "Macho_AMD64"));

            Assert.IsNotNull(result);

            Assert.IsTrue(result.Count(a => a.Format == MachoFormat.AMD64) == 1);
        }
    }
}