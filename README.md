# myPortfolio

An application used for storing and displaying user applications and software projects, built with WPF, XAML, C#, and PostgreSQL.

## Project Status

This project is currently in development. Users can now sign up to create an account, as well as edit own profile information. If the user has administrative access, they can add games or external applications to this application.

## Project Screen Shot(s)

![Sign In Screen (Start Screen)](/images/one.jpg?raw=true "Optional Title")


![Sign Up Screen](/images/two.jpg?raw=true "Optional Title")

![Update Profile Screen](/images/three.jpg?raw=true "Optional Title")

## Installation and Setup Instructions

Since the project is in development, it is currently not possible to install and try this application from another machine. However, the code can be cloned then viewed from preferred IDE.

## Reflection

This is a personal project meant to gain experience with the .NET Framework. 

This was originally named RZ1 (RhamZtation) meant to be a glorified Steam application, where it's possible to add my personal game projects to. Later on in deciding the requirements for this project, I've decided that I wanted to make this application more like a digital protfolio application, hence naming it "myPortfolio." 

Deciding an environment was no 'one and done' task. A lot of trial and error came from choosing a framework to stick with. I've tried C++ with Qt, and Java with JavaFX, but I stuck with C# with WPF for now.

C# and XAML was languages I wasn't too familiar with. My strongest language is Java, so learning C# wasn't too much of a learning curve. With XAML and its similarities to HTML, I haven't had too many issues learning about that markup language.

The biggest, and possibly my most unexpected, learning curve was to learn a completely new architechtural style that is most suitable for WPF applications, MVVM. I've had experience developing applications within a MVC framework, so I had some crutch. However, distinguishing the difference between a ViewModel and Controller took some time, but now I have a strong idea on their distinction.

Currently, the application is being developed in Visual Studio, using XAML as the markup language, C# as the programming language, with various NuGet extensions to be easily access images/icons, and to connect to an external PostgreSQL database with all the user and app information. When I continue devloping this application, I hope to migrate the database from PostgreSQL to SQL Server so the application can solely be .NET oriented, and finish designing the features of other portions of the project.

This was a 3 week long project built during my third module at Turing School of Software and Design. Project goals included using technologies learned up until this point and familiarizing myself with documentation for new features.  
