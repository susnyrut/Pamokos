using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyWebApplication.Models.CatViewModels;

namespace MyWebApplication.Controllers
{
    /*
        * Start off by creating the backend logic which allows you to access the views via browser. 
        * To add a controller go to Solution explorer ->  right click controllers folder -> Add -> Controller... -> click Add button
        * Note that name of the controller matters. The link that you will access the controllers actions, depend on controller name. If necessary, you can set up custom routing
            to be able to map the URL you wish to a specific controller action.

        * Create a controller action to be able to view your page in the browser. If no data needs to be passed, a simple [return View();] as body of the action will do it.
        * If data needs to be passed, as seen in the actions of the current controller, simply add them as View() method parameters.
        * Action names do matter as well. The default URL mapping to controller action follows these rules -> localhost:[port]/{controller name - "Controller"}/{Action name}

        * You should replicate the location of the view to match the location of the controller. Index action finds its view because the path for both view and controller are similar.
            The path for Controller is Controllers/Cats/CatsController.cs. The path for the index view is Views/Cats/Index.cshtml. As you may have noticed the view name should match the controller's
            action name.
        * If it is necessary you can explictly specify which view the controller action needs to load by passing a string parameter as seen in Cat action.

        * Continue to View/Cats/Index.cshml for further tips.
    */
    public class CatsController : Controller
    {
        List<Cat> Cats = new List<Cat>()
        {
            new Cat()
            {
                Id = Guid.Parse("dcf62269-cf01-4551-9c32-3a618e497ab2"),
                Title = "Cat in a bottle",
                Description = "I R Cat in ze bottle. Fear my mighty meow!",
                ImageSmallUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQjWLw32X5V5KD5t6wsUBNlqsyQJ5EGd6THd0iVoYwrRQ_UHdAq3g",
                ImageUrl = "http://monorailcat.com/wp-content/uploads/2014/10/funny-liquid-cats-17.jpg"
            },
            new Cat()
            {
                Id = Guid.Parse("eec1932d-ed54-4470-a933-7da79cfd3dd0"),
                Title = "Dune cat",
                Description = "I are dunecat. I controls the spice. I controls the universe.",
                ImageSmallUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRVfElsnM1hTkVb1mql0-IL6mwLijp2MdzGL4cQyPbAqIy1rKDN",
                ImageUrl = "http://cdn2-www.cattime.com/assets/uploads/gallery/25-funny-cat-memes/021-FUNNY-CAT-MEME.jpg"
            },
            new Cat()
            {
                Id = Guid.Parse("74dccaf8-9da6-47af-a828-58bda356731b"),
                Title = "Joker cat",
                Description = "Why so serious, son?!",
                ImageSmallUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcQt76lWT_eRGMPr47W8o3L4JZLASmGPD7_jc6r34tIt70EpvFOH",
                ImageUrl = "http://4.bp.blogspot.com/-C13G0vT9dy0/VnmoRVk7ldI/AAAAAAAACTs/DzKad8c2dAI/s640/funny-cat-pictures-027-021.jpg"
            },
        };

        public ActionResult Index()
        {
            List<CatListViewModel> catsList = new List<CatListViewModel>();

            foreach (Cat cat in Cats)
            {
                catsList.Add(new CatListViewModel() { Id = cat.Id, Title = cat.Title, ImageUrl = cat.ImageSmallUrl });
            }

            return View(catsList); /* catsList parameter is passed to View() method. This list will be available in the Model property inside the view. */
        }

        public ActionResult Cat(Guid catId)
        {
            Cat selectedCat = Cats.Single(item => item.Id == catId);
            CatDetailsViewModel catViewModel = new CatDetailsViewModel()
            {
                Description = selectedCat.Description,
                ImageUrl = selectedCat.ImageUrl,
                Title = selectedCat.Title
            };

            return View("~/Views/Cats/Cat.cshtml", catViewModel); /* Explicitly specified view. It would work without the first string parameter, too!*/
        }
    }

    public class Cat
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageSmallUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}