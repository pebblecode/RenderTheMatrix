using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RenderTheMatrix.Web;
using RenderTheMatrix.Web.Controllers;
using Xunit;

namespace RenderTheMatrixTests
{
    public class GitRepositoryAccessTests
    {
        [Fact]
        public void CanAccessRepository()
        {
            var commits = new GitCommits();
            Assert.NotNull(commits);
        }
    }
}
