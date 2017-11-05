using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDungeon.Game.Input;
using System.Windows.Forms;

namespace SharpDungeon.Test {
    [TestClass]
    public class KeyManagerTest {

        //Testing isDown() and isUp() methods
        [TestMethod]
        public void KeysTest() {
            KeyManager keyManager = new KeyManager();

            keyManager.KeyDown(this, new KeyEventArgs(Keys.A));
            keyManager.tick();
            Assert.AreEqual(true, keyManager.isDown(Keys.A));
            Assert.AreEqual(false, keyManager.isUp(Keys.A));

            keyManager.KeyUp(this, new KeyEventArgs(Keys.A));
            keyManager.tick();
            Assert.AreEqual(false, keyManager.isDown(Keys.A));
            Assert.AreEqual(true, keyManager.isUp(Keys.A));
        }

        //Testing keys, which is just pressed once
        [TestMethod]
        public void KeysPressedTest() {
            KeyManager keyManager = new KeyManager();

            keyManager.KeyDown(this, new KeyEventArgs(Keys.A));
            keyManager.tick();
            Assert.AreEqual(true, keyManager.isPressed(Keys.A));

            keyManager.KeyDown(this, new KeyEventArgs(Keys.A));
            keyManager.tick();
            Assert.AreEqual(false, keyManager.isPressed(Keys.A));

            keyManager.KeyUp(this, new KeyEventArgs(Keys.A));
            keyManager.tick();
            Assert.AreEqual(false, keyManager.isPressed(Keys.A));
        }

    }
}
