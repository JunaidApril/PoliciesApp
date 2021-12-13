# Full Stack Developer Technical Test

## Purpose and Instructions

### Purpose

As it is difficult to fully assess somebody’s abilities at an interview, particularly their programming skills, we give a small programming exercise to all potential recruits. The problem is a fairly simple one, which should be completed in the applicant’s own time.

This is an opportunity for the candidate to show their engineering knowledge and craft work.  We are looking for engineering best practice. The test will be scored accordingly. 

This is a very important part of our hiring process. Therefore we recommend that candidates give this adequate consideration and address this task as they would do for any other professional assignments in their current workplace.

### Instructions 

-   We use GitHub to host these tests to create a modern practical engineering experience. Please complete this exercise and create a pull request containing your solution 
-   We recommend you create a development branch for your development and from this create a final pull request to the master branch for review
-   We would much prefer that you submit a complete software implementation that demonstrates modern engineering best practices.  However we also appreciate that to provide a complete solution we may be expecting too much of the candidates time. If you are pressed for time, we recommend you use the pull request to comment on areas that, given more time, you would address or have done differently. 
-   Please update the RUNME.MD file with instructions how to run your application 

**Please note that your submission must only contain your own work.  Under no circumstances should your submission contain any content owned (in whole or in part) by a third party except where you are expressly permitted to do so by the relevant third party, for example an open-source library creators.**

## Exercise

### Introduction

The solution contains a standard Asp.Net Core project with an Angular cli project. Using Visual Studio F5 will build both the server side and client side code and launch the website.

Once running the client side code (found in ClientApp/src) can be edited in Visual Studio or any other IDE (e.g. VSCode) and will be rebuilt as files are changed

There is a single server side API controller called PolicyController that has a very simple implementation of a repository for storing policies of the following format:

Each policy has the following structure and data classes are already provided:
```
{
	PolicyNumber: int,
	PolicyHolder: {
		Name: string,
		Age: int,
		Gender: emum
	}
}
```

### Requirements:
1. A website is required that will display all policies from the repository.
2. It should be possible to do CRUD operations on Policies.
3. It is up to you how you design the website and CRUD operations.
4. You may use existing CSS/frameworks to create a professional looking front end.


### General Considerations
Below are some hints to help with this exercise. 
-   Demonstrate engineering best practice 
-   Use the pull request to provide a 'self review' to highlight any assumptions or potential future refactoring
