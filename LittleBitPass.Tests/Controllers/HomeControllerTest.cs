using System;
using System.Web.Mvc;
using LittleBitPass.Controllers;
using NUnit.Framework;

namespace LittleBitPass.Tests.Controllers
{
	[TestFixture]
	public class HomeControllerTest
	{
		[Test]
		public void Index ()
		{
			// Arrange
			var controller = new HomeController ();

			// Act
			var result = (ViewResult)controller.Index ();

			var mvcName = typeof(Controller).Assembly.GetName ();
			var isMono = Type.GetType ("Mono.Runtime") != null;

			var expectedVersion = mvcName.Version.Major;
			var expectedRuntime = isMono ? "Mono" : ".NET";

			// Assert
			Assert.AreEqual (expectedVersion, result.ViewData ["Version"]);
			Assert.AreEqual (expectedRuntime, result.ViewData ["Runtime"]);
		}
	}
}
