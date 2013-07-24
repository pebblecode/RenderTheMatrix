using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibGit2Sharp;


namespace RenderTheMatrix.Web.Controllers
{
    public class MatrixController : Controller
    {
        private GitCommits _gitCommits;

        public MatrixController()
        {
            this._gitCommits = new GitCommits();
        }

        public ActionResult Render()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetColumns(int columnCount)
        {
            var columnData =
                Enumerable.Range(0, columnCount)
                .Select(_ => _gitCommits.GetNextCommit());

            return Json(new { columns = columnData }, JsonRequestBehavior.AllowGet);
        }
    }

    public class GitCommits
    {
        private Repository _gitRepository;

        private const string RepositoryPath = @"C:\repo";

        public GitCommits()
        {
            this._gitRepository = new Repository(RepositoryPath);
        }

        public IEnumerable<int> GetNextCommit()
        {
            throw new NotImplementedException();
        }
    }
}
