using Ritmus.Business.Manager.Repository;
using Ritmus.Data.Entity;
using Ritmus.Web.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Ritmus.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            ProductChartVM productChartVM = new ProductChartVM();

            List<ProductVM> model = unit.ProductRepo.GetAll().Select(x => new ProductVM()
            {
                ID = x.ID,
                Name = x.Name,
                Price = x.Price,
                ImageURL = x.ImageURL,
                Stock = x.Stock,
                
            }).ToList();

            List<ChartVM> chartVMs = unit.ChartRepo.GetAll().Select(x => new ChartVM()
            {
                ID=x.ID,
                Quantity = x.Quantity,
                //ProductPrice=x.Products.Price,
                ProductID=x.ProductID,
                ProductImgURL=x.Products.ImageURL,
                ProductName=x.Products.Name,
                TotalPrice=(x.Products.Price)*(x.Quantity),
            }).ToList();


            productChartVM.products = model;
            productChartVM.charts = chartVMs;
            productChartVM.ChartPrice = chartVMs.Sum(x => x.TotalPrice);
            return View(productChartVM);
        }

        public ActionResult AddToChart(int id)
        {
            
            Chart chart = unit.ChartRepo.FirstOrDefault(x => x.ProductID == id);
            ChartVM model = new ChartVM();

            //eğer eklemek istenen ürün daha önceden hiç sepete eklenmediyse çalışan alan
            if ( chart== null)
            {
                Chart chartDB = new Chart();
                Product product = unit.ProductRepo.FirstOrDefault(x => x.ID == id);
              
                chartDB.IsDeleted = false;
                chartDB.ProductID = product.ID;
                chartDB.Quantity = 1;
                chartDB.TotalPrice = product.Price;
                unit.ChartRepo.Add(chartDB);


                model.ProductID = product.ID;
                model.ProductImgURL = product.ImageURL;
                model.ProductName = product.Name;
                model.ProductPrice = product.Price;
                model.TotalPrice = chartDB.TotalPrice;
                model.Quantity = 1;
                model.ID = chartDB.ID;

                
                return Json(model,JsonRequestBehavior.AllowGet);
            }

            //Ürün daha önce sepete eklenmiş ise çalışan alan. burda miktar arttırıyoruz 
            else
            {
                

                model.ID = chart.ID;
                model.ProductID = chart.ProductID;
                model.ProductImgURL = chart.Products.ImageURL;
                model.ProductName = chart.Products.Name;
                model.Quantity = chart.Quantity + 1;
                model.TotalPrice = (chart.Products.Price)*(model.Quantity);

                chart.TotalPrice = model.TotalPrice;
                chart.Quantity += 1;
                unit.ChartRepo.Update(chart);

                return Json(model,JsonRequestBehavior.AllowGet);
            }

         
        }

       public ActionResult Increase(int id)
        {
            Chart chart = unit.ChartRepo.FirstOrDefault(x => x.ID == id);
            int Price= unit.ProductRepo.FirstOrDefault(x => x.ID == chart.ProductID).Price;
            ChartVM model = new ChartVM();
            chart.Quantity += 1;
            chart.TotalPrice= (Price * chart.Quantity);
            unit.ChartRepo.Update(chart);

            model.ID = chart.ID;
            model.ProductID = chart.ProductID;
            model.Quantity = chart.Quantity;
            model.TotalPrice = chart.TotalPrice;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Decrease (int id)
        {
            Chart chart = unit.ChartRepo.FirstOrDefault(x => x.ID == id);
            int Price = unit.ProductRepo.FirstOrDefault(x => x.ID == chart.ProductID).Price;
            ChartVM model = new ChartVM();
            chart.Quantity -= 1;
            chart.TotalPrice= (Price * chart.Quantity);
            unit.ChartRepo.Update(chart);

            model.ID = chart.ID;
            model.ProductID = chart.ProductID;
            model.Quantity = chart.Quantity;
            model.TotalPrice = chart.TotalPrice;
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
           Chart model=unit.ChartRepo.FirstOrDefault(x => x.ID == id);

            model.IsDeleted = true;
            unit.ChartRepo.Update(model);

            return Json(true,JsonRequestBehavior.AllowGet);
        }

    }
}