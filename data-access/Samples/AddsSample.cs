// using System;
// using System.Linq;
// using DAL.Models;

// namespace data_access{
//     public class AddAdds
//     {
//         public static void Init(TurradgiverContext context){
//             // context.Database.EnsureCreated();
//             if (context.Adds.Any()){
//                 return;
//             }
//             var adds = new Adds[]
//             {
//                 new Adds{Id=1,Name="Las Vegas BABY", Description="supervoyage de ouf je vous conseille de venir",Rate=0},
//                 new Adds{Id=2,Name="Las Vegas BABY", Description="supervoyage de ouf je vous conseille de venir",Rate=0},
//                 new Adds{Id=3,Name="Las Vegas BABY", Description="supervoyage de ouf je vous conseille de venir",Rate=0},
//                 new Adds{Id=4,Name="Las Vegas BABY", Description="supervoyage de ouf je vous conseille de venir",Rate=0},
//                 new Adds{Id=5,Name="Las Vegas BABY", Description="supervoyage de ouf je vous conseille de venir",Rate=0},
//                 new Adds{Id=6,Name="Las Vegas BABY", Description="supervoyage de ouf je vous conseille de venir",Rate=0}
//             };
//             foreach (Adds a in adds)
//             {
//                 context.Adds.Add(a);
//             }
//             context.SaveChanges();
//         }
//     }
// }