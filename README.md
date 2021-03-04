# Lets Learn TDD
This is a small demo repo to check out and follow allong with the instructions below

## What is TDD?
TDD stands for Test Driven Development.

## The TDD process and why the steps are important
* Write a failing test and see it fail so we know we have written a relevant test for our requirements and seen that it produces an easy to understand description of the failure
* Writing the smallest amount of code to make it pass so we know we have working software
* Then refactor, backed with the safety of our tests to ensure we have well-crafted code that is easy to work with

## Dicipline
1. Write a test
1. Make the compiler pass
1. Run the test, see that it fails and check the error message is meaningful
1. Write enough code to make the test pass
1. Refactor

## Refactoring
When refactoring code you must not be changing behaviour

## What are we going to do?
We are going to create a test for a simple hello world end point that should return the users name.

## First Steps
Install npm modules
```
cd LetsLearnTDD
npm install
```
Run the Application (from the `LetsLearnTDD` folder):
```
dotnet run
```
Confirm application can be accessed on local host.
Go to the 'Hello' link and show that only `Loading...` is shown as we have not put the code in the backend yet.

## Before we begin
Before we create the controller and have a fully running solution lets start small and add out first test.
The stages we are going to go through are:
1. Get our tests to build
1. Get tests to pass
1. Refactor

We need to think about what our app is and what we expect it to do so lets list our end goal:  
**A webpage where a user enters the their details, this data will be formated and returned to the user with additional details found about the user or performed by the server**
The above will guide our development and test efforts.

Lets start with an easy test that will only make sure our Model has a Name field, this will form the basis that everything is built on. You may ask why are we choosing this as our starting point?
1. The front end needs to send the data to the controller so we want a controller before the front end
1. The controller needs to work with the data so it needs to have a Model to work with

For the keen eyes we could do the reverse and start with the Front End code, however a lot of Front End work is connecting to back end api's so I figured we start by creating the Back End API first.

**Q: Do we need all these tests???** A senior developer I know perfectly summed this up for me: `Think of code as a cost, unless it has value it is just a cost`  
**A: No!** but to learn how to write the code with tests we should start with some easy first steps and we can then refactor out tests that are not needed later when we identify it has no value. As you progress you will learn to identify which code adds value and which code is just a cost. 

## First test
In the `LetsLearnTDD.Tests` folder, create a folder called `Models` and create a file called `HelloWorldTest.cs`:  [LetsLearnTDD.Tests/Models/HelloWorldTest.cs](LetsLearnTDD.Tests/Models/HelloWorldTest.cs)

In the file create the NUnit test class as below:
```csharp
using NUnit.Framework;

namespace LetsLearnTDD.Tests.Models
{
   public class Tests
   {
   } 
}
```
Inside our `Tests` class create our first test method:
```csharp
       [Test]
       public void HelloWorldWithGivenName()
       {
       }
```

Now think back to doing the bare minumum, all we want to do here is test that our Model has a name and the created model will have the same name as our test. Lets call our model `HelloWorld` and add it to the test:
```csharp
       [Test]
       public void HelloWorldWithGivenName()
       {
           const string testname = "Unicorn"
           new helloWorlModel = HelloWorld()
           Assert.AreEqual(testname, helloWorldModel.Name)
       }
```
The completed file should look like this now:
```csharp
using NUnit.Framework;

namespace LetsLearnTDD.Tests.Models
{
   public class Tests
   {
       [Test]
       public void HelloWorldWithGivenName()
       {
           const string helloWorldName = "Unicorn";
           var helloWorldModel = new HelloWorld();
           Assert.AreEqual(helloWorldName, helloWorldModel.Name);
       }
   } 
}
```
In the `LetsLearnTDD.Tests` folder try to build your tests with `dotnet build`, you should end up with an error message containing:  
<span style="color:red">**Models\HelloWorldTest.cs(11,38): error CS0246: The type or namespace name 'HelloWorld' could not be found (are you missing a using directive or an assembly reference?)**</span>

This is because we don't have a model yet, so lets do the minumum required to make this build. We want to spend as little time with none building tests so it is the first step!

In the `LetsLearnTDD\Models` folder create a file called `HelloWorld.cs`.

All we want at this stage is the basic that will make our test build so we will create an empty class in our `HelloWorld.cs`
```csharp
namespace LetsLearnTDD.Models
{
   public class HelloWorld{} 
}
```
Now in your [LetsLeardTDD.Tests/Models/HelloWorldTest.cs](LetsLeardTDD.Tests/Models/HelloWorldTest.cs) file you can add `using LetsLearnTDD.Models` namespace to the top of the file:
```csharp
using NUnit.Framework;
using LetsLearnTDD.Models;
```
Now try and build your tests again and check the error. You should get the below which is telling us we need add `Name` to our model:

<span style="color:red">**Models\HelloWorldTest.cs(13,60): error CS1061: 'HelloWorld' does not contain a definition for 'Name' ...**</span>

So lets add it to our HelloWorld model:
```csharp
namespace LetsLearnTDD.Models
{
   public class HelloWorld
   {
       public string Name {get; set;}
   } 
}
```

After saving the file if we run `dotnet build` again it should all pass! Yay we have our tests building, now we need to get them to pass.

Run `dotnet test` in the `LetsLearnTDD.Tests` folder and inspect the error message.
```
Error Message:
    Expected: "Unicorn"
But was:  null
```

This is because we initially just wanted our tests to build, we now need to modify the `HelloWorldTest.cs` file and initialise our model with the name. Our test should now look like this:
```csharp
        [Test]
        public void HelloWorldWithGivenName()
        {
            const string helloWorldName = "Unicorn";
            var helloWorldModel = new HelloWorld() { Name = helloWorldName };
            Assert.AreEqual(helloWorldName, helloWorldModel.Name);
        }

```
And if we run our tests again with `dotnet test` everything should pass.

Contratulations on your first bit of C# TDD!

## Controller Testing
We have so far written the most basic test which allows us to test the model we eventually want to return to the front end.
To send this model we will need a controller so lets go ahead and create the following directory and file: [LetsLearnTDD.Tests/Controllers/HelloWorldControllerTest.cs](LetsLearnTDD.Tests/Controllers/HelloWorldControllerTest.cs)

First add our test boiler plate
```csharp
using NUnit.Framework;

namespace LetsLearnTDD.Tests.Controllers
{
    public class Tests
    {
    }
}
```
Before we add the test, lets review the process:
1. Write the smallest increment of test possible
1. Get it to build
1. Get it to pass
1. Refactor

The last step hasn't been required yet but we will revisit it soon.

So as a first step lets make sure we can get an ok reponse from the controller. Add the follow to the file we just created:
```csharp
using NUnit.Framework;

namespace LetsLearnTDD.Tests.Controllers
{
    public class Tests
    {
        [Test]
        public void TestGet()
        {
            var controller = new HelloWorldController();
            var response = controller.Get();
            Assert.AreEqual(200, response.StatusCode);
        }
    }
}
```

In the above file we have added a `TestGet` method which will only care we get a valid `200` Ok reponse from the controller. We create a new Controller for our testing and then call the `Get` Method.

Try to build your tests again and note the output.

We get the `type or namespace name 'HelloWorldController'` not found, Lets gets this test to build by adding this controller.

Create the following file [LetsLearnTDD/Controllers/HelloWorldController.cs](LetsLearnTDD/Controllers/HelloWorldController.cs)

And add our Controller boiler plate below:
```csharp
using Microsoft.AspNetCore.Mvc;

namespace LetsLearnTDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        public HelloWorldController() {}
    }
```

We can now add this namespace to our `HelloWorldControllerTests.cs`:
```csharp
using NUnit.Framework;
using LetsLearnTDD.Controllers; 
```

Lets build our tests again and see if there are more errors.
We should have an error saying we do not have a Get method so lets add it now below our constructor except to confirm we get a failure on a bad call lets initially send a `BadRequest Object`:
```csharp
        [HttpGet]
        public BadRequestResult Get()
        {
            return BadRequest();
        }
```

The `HelloWorldController.cs` file should now look like this:
```csharp
using Microsoft.AspNetCore.Mvc;

namespace LetsLearnTDD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        public HelloWorldController() {} 

        [HttpGet]
        public BadRequestResult Get()
        {
            return BadRequest();
        }
    }
}
```

Running dotnet build should now pass! But do our tests???

Run `dotnet test` and check the result, we expect a failure as we are checking for a good result but actually returning a bad result. This is to confirm the test is not an `Evergreen` test that will always pass and allow us to focus on getting the tests to build first.

You should get the following:
```
Error Message:
    Expected: 200
But was:  400
```

Lets now get the tests to pass by changing the hello world `Get` method to return a `OkResult`
```csharp
        [HttpGet]
        public OkResult Get()
        {
            return Ok();
        }
```

Now when we run `dotnet test` all our test should pass.

You have now completed your first controller test!

## Next steps
1. Test setup and tear down
1. Dependancy Injection