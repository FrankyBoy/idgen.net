using Microsoft.VisualStudio.TestTools.UnitTesting;
using idgen.net;

namespace Tests
{
    [TestClass]
    public class NieNifTests
    {
        [TestMethod]
        public void ValidatesItsOwnDnis()
        {
            var id = IdGen.GenerateDni();
            Assert.AreEqual(IdGen.ValidateDni(id), ValidationResult.Valid);
        }
        [TestMethod]
        public void ValidatesItsOwnNif()
        {
            var id = IdGen.GenerateNif();
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.Valid);
        }
        [TestMethod]
        public void ValidatesItsOwnNie()
        {
            var id = IdGen.GenerateNie();
            Assert.AreEqual(IdGen.ValidateNie(id), ValidationResult.Valid);
        }
        [TestMethod]
        public void DniNifAreSame()
        {
            var id = IdGen.GenerateNif();
            Assert.AreEqual(IdGen.ValidateDni(id), ValidationResult.Valid);

            id = IdGen.GenerateDni();
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.Valid);
        }
        [TestMethod]
        public void NieNifAreNotSame()
        {
            var id = IdGen.GenerateNie();
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.PatternMismatch);

            id = IdGen.GenerateNif();
            Assert.AreEqual(IdGen.ValidateNie(id), ValidationResult.PatternMismatch);
        }

        [TestMethod]
        public void ValidatesRealNif()
        {
            const string id = "05291916F";
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.Valid);
        }

        [TestMethod]
        public void NotValidatesBadChecksumNif()
        {
            const string id = "05291916A";
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.ChecksumMismatch);
        }

        [TestMethod]
        public void NotValidatesBadLengthNif()
        {
            const string id = "005291916F";
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.PatternMismatch);
        }

        [TestMethod]
        public void NotValidatesBadLengthNif2()
        {
            const string id = "5291916F";
            Assert.AreEqual(IdGen.ValidateNif(id), ValidationResult.PatternMismatch);
        }

        [TestMethod]
        public void ValidatesRealNie()
        {
            const string id = "X1233453D";
            Assert.AreEqual(IdGen.ValidateNie(id), ValidationResult.Valid);
        }

        [TestMethod]
        public void NotValidatesBadLengthNie()
        {
            const string id = "X12233453D";
            Assert.AreEqual(IdGen.ValidateNie(id), ValidationResult.PatternMismatch);
        }

        [TestMethod]
        public void NotValidatesBadLengthNie2()
        {
            const string id = "X123453D";
            Assert.AreEqual(IdGen.ValidateNie(id), ValidationResult.PatternMismatch);
        }
    }
}