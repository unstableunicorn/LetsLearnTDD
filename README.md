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

## Develop The Controller
### First test
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

**Note:** *Testing the Model is an Anti-Pattern as it is not required. We will leave this in as it was an introduction and basic first test to write. Our Controller tests should pick up any changes to the Model and we can correct those as we progress.*

### Controller Testing
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
We knew this hoever it is good practise to write a test that will fail before we change our code to make it pass.

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

Let's now build on this, we don't want just an ok reponse, we would like to return our HelloWorld Model which contains our Name.

Let start by adding another test to check if our controller returns a default Name of `Unicorn`. In your `HelloWorldControllerTest.cs` add a new test for this:
```csharp
        [Test]
        public void TestDefaultNameGet()
        {
            var controller = new HelloWorldController();
            var response = controller.Get();
            Assert.AreEqual("Unicorn", response.Name);
        }
```
Try to build the tests with `dotnet build` and note the output says the reponse `OkResult` does not have a Name. Let's make our Controller return the default object. In the `HelloWorldController.cs` file change our get method to below and return a HelloWorld Model(note we need to use the Model namespace so add it to the top):
```csharp
using LetsLearnTDD.Models;
```
```csharp
        [HttpGet]
        public HelloWorld Get()
        {
            return new HelloWorld();
        }
```
Run build again and check the output.
Our first test now does not have a StatusCode definition, we need to wrap our HelloWorld in an OkObjectResult so we can check the status code.
```csharp
        public OkObjectResult Get()
        {
            return new OkObjectResult(new HelloWorld());
        }
```
We will also need to update our new test to cast our response value as a HelloWorld Model, `HelloWorldControllerTests.cs`:
```csharp
        public void TestDefaultNameGet()
        {
            var controller = new HelloWorldController();
            var response = controller.Get().Value as HelloWorld;
            Assert.AreEqual("Unicorn", response.Name);
        }
```
Running build should now pass, however running test will fail with the below:
```
  X TestDefaultNameGet [142ms]
  Error Message:
     Expected: "Unicorn"
  But was:  null
```
We can now set the value our get method and return it.

*To make life a little easier `dotnet` has a built in `watch` that will monitor the project for file changes and auto rerun tests. This can be make life a little easier by making it so you don't need to run them manually. Also by default it builds things first so we can focus on having it build and then getting the test to pass. Run it now with:*
```
dotnet watch test
```

In our HelloWorld Controller check our get method to return a new model with the Name "Unicorn":
```csharp
        public OkObjectResult Get()
        {
            var helloWorld = new HelloWorld{Name = "Unicorn"};
            return new OkObjectResult(helloWorld);
        }
```

The `dotnet watch test` command should automatically pick up the change on save and show you we now have 3 tests passing!

### Refactor
We don't have much to refactor at this point, if we image that we will have a lot of tests there are a few things to look at.

First we have given all our test classes a Generic Name of `Tests`. This may be fine if our project only has one controller and no other tests however as our project grows we should make sure each test class has an appropriate name.

Lets change our `HelloWorldTest.cs` to have the following class name:
```csharp
    public class HelloWorldModelTests
```
And similarily for our controller tests:
```csharp
    public class HelloWorlControllerTests
```

Next if we look at the controller tests we are repeating our selves a bit. We can move our controller creation in to a setup class and then reference that in each test like so:
```csharp
    public class HelloWorlControllerTests
    {
        HelloWorldController Controller;

        [SetUp]
        public void SetUp()
        {
            Controller = new HelloWorldController();
        }
...
```
We can now remove that from each test and just reference the `Controller`, our final Controller test file should now match:
```csharp
using NUnit.Framework;
using LetsLearnTDD.Controllers;
using LetsLearnTDD.Models;

namespace LetsLearnTDD.Tests.Controllers
{
    public class HelloWorlControllerTests
    {
        HelloWorldController Controller;

        [SetUp]
        public void SetUp()
        {
            Controller = new HelloWorldController();
        }

        [Test]
        public void TestGet()
        {
            var response = Controller.Get();
            Assert.AreEqual(200, response.StatusCode);
        }

        [Test]
        public void TestDefaultNameGet()
        {
            var response = Controller.Get().Value as HelloWorld;
            Assert.AreEqual("Unicorn", response.Name);
        }
    }
}
```

And running the tests again everything should pass.

We now have a good base to continue writing small increments to our test.

### Testing our endpoint
We have yet to add it to the front end so how can we simple test it?
For the moment stop your tests from being watched and lets run the application. Navigate to the `LetsLearnTDD` project folder and type `dotnet run`
Once it is up and running use *cURL* to send a get request and you should get the following:

```bash
curl https://localhost:5001/helloworld
{"name":"Unicorn"}
```
At this stage we have finished our basic back end. We can now move on to the front end and add out first increment or continue with developing and testing the back end until it is complete.

For this turorial we are now going to move to developing the front end.

## Next steps
1. Test setup and tear down
1. Dependancy Injection