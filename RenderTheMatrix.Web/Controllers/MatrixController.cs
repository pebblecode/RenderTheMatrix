using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RenderTheMatrix.Web.Controllers
{
    public class MatrixController : Controller
    {
        private readonly GitCommits _gitCommits;

        public MatrixController()
        {
            this._gitCommits = GitCommits.Instance();
        }

        public ActionResult Render()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "columnCount")]
        public JsonResult GetColumns(int columnCount)
        {
            var columnData =
                Enumerable.Range(0, columnCount)
                .Select(_ => _gitCommits.GetNextCommit());

            return Json(new { columns = columnData }, JsonRequestBehavior.AllowGet);
        }
    }
}
