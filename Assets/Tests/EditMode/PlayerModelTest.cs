using NUnit.Framework;
using Player;

namespace Tests.EditMode
{
    public class PlayerModelTest
    {
        private readonly PlayerModel _model = new("player");

        [Test]
        public void PlayerModel_Apply_MaxPoints_CapsAt999()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            for (int i = 0; i < 999; i++)
            {
                _model.X1Plus();
                _model.Apply();
            }

            // Assert
            Assert.AreEqual(999, _model.Points);
        }

        [Test]
        public void PlayerModel_Apply_MinPoints_CapsAtNegative99()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            for (int i = 0; i < 999; i++)
            {
                _model.X1Minus();
                _model.Apply();
            }

            // Assert
            Assert.AreEqual(-99, _model.Points);
        }

        [Test]
        public void PlayerModel_Apply_DecreaseBy1_SetsPointsTo49()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            _model.X1Minus();
            _model.Apply();

            // Assert
            Assert.AreEqual(49, _model.Points);
        }

        [Test]
        public void PlayerModel_Apply_DecreaseBy5_SetsPointsTo45()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            _model.X5Minus();
            _model.Apply();

            // Assert
            Assert.AreEqual(45, _model.Points);
        }

        [Test]
        public void PlayerModel_Apply_IncreaseBy1_SetsPointsTo51()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            _model.X1Plus();
            _model.Apply();

            // Assert
            Assert.AreEqual(51, _model.Points);
        }

        [Test]
        public void PlayerModel_Apply_IncreaseBy5_SetsPointsTo55()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            _model.X5Plus();
            _model.Apply();

            // Assert
            Assert.AreEqual(55, _model.Points);
        }

        [Test]
        public void PlayerModel_Counter_IncreaseBy10_PointsSetsTo10()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            for (int i = 0; i < 2; i++)
                _model.X5Plus();

            // Assert
            Assert.AreEqual(10, _model.Counter);
        }

        [Test]
        public void PlayerModel_Counter_MaximumValueIs99()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            for (int i = 0; i < 100; i++)
                _model.X1Plus();

            // Assert
            Assert.AreEqual(99, _model.Counter);
        }

        [Test]
        public void PlayerModel_Counter_DecreaseBy10_PointsSetsToNegative10()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            for (int i = 0; i < 2; i++)
                _model.X5Minus();

            // Assert
            Assert.AreEqual(-10, _model.Counter);
        }

        [Test]
        public void PlayerModel_Counter_MinimumValueIsNegative99()
        {
            // Arrange
            _model.Restart();
            _model.Clear();

            // Act
            for (int i = 0; i < 100; i++)
                _model.X1Minus();

            // Assert
            Assert.AreEqual(-99, _model.Counter);
        }
    }
}
