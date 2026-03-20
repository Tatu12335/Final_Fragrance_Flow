Fragrance-Flow - Fullstack app for fragrance collectors, for analysing and maintaining.

#Tech Stack : 
 
  • Backend 
    ° .NET 8, C#, Dapper
  • Frontend
    ° React[Coming soon], WPF(MahApps.Metro), Cli
  • Database 
    ° MSSQL
  • Auth 
    ° JWT(Json Web Token)

#Key Features : 

  • User management & Secure login ( JWT ) 
  • Fragrance & User Database
  • Suggestion algorithms [ Still WIP ]
  • Role-Based Access Control ( RBAC )

#Architeture : 
  |
  Api
  |————Controllers/
  |    |——AuthController.cs
  |    |——FragranceController.cs
  |    |——UserController.cs
  |    |——AdminController.cs
  |    
  |————Dtos/
  |    |——Auth/
  |    |——User/
  |    |——Fragrance/
  |    |——Admin/
  |
  |—————Middleware/
  |
  |—————Program.cs
  |
  |—————Application/
  |     |——Interfaces/
  |     |——Services/
  |     
  |—————Domain
  |     |——Entities/
  |    
  |—————Infrastructure/
  |     |——Data/ 
  |     |——Repositories/
  |     |——Security/
  |     |——Service/
  |
  Cli
  |
  |
  |
  Wpf
  |
  |

 # Lessons Learned
      • Throught this project i have learned a lot about managing bigger more complex system's and, 
      Also about database operations,security-practices and writing more readable code.

      • This project has taught me JWT, Which i think is very important in modern software development.

      • As this project has grown bigger and bigger through out these 100, or so hours i have put in to it. I have 
      learned how big of a role architeture and planing plays in the project.

      • I have also learned a lot about secure coding practices. I always have considered myself as an 'Security Firts'
      Programmer but, this project has really taught me how do real world applications, protect themselves from threats.

      • My biggest problem through this project has been the JWT Implementation because, It felt kinda complicated at first and at that point the 
      project was already pretty big, about 2000 lines of code. So It was pretty difficult to implement JWT but, I did my research and fought through 
      all the bugs and eventually i got it.
      
