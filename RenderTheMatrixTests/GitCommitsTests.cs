using System;
using System.Collections.Generic;
using System.Linq;
using RenderTheMatrix.Web;
using Xunit;

namespace RenderTheMatrixTests
{
    [Trait("Category", "Unit")]
    public class GitCommitsTests : IUseFixture<GitCommitsFixture>
    {
        private GitCommits _commits;

        [Fact]
        public void CanInstantiate()
        {
            this._commits = GitCommits.Instance();
            Assert.NotNull(_commits);
        }

        [Fact]
        public void CanGetCommits()
        {
            this._commits = GitCommits.Instance();
            Assert.DoesNotThrow(() => _commits.GetNextCommit());
        }

        [Fact]
        public void HasAmountOfItems()
        {
            this._commits = GitCommits.Instance();
            var commits = _commits.GetNextCommit();
            Assert.Equal(commits.Length, GitCommits.BufferSize);
        }

        [Fact]
        public void CanCallMultiple()
        {
            this._commits = GitCommits.Instance();
            for (int i = 0; i < 20; i++)
            {
                var commit = _commits.GetNextCommit();
                Assert.NotNull(commit);
            }
        }

        [Fact]
        public void DataNotSame()
        {
            this._commits = GitCommits.Instance();
            var commits = new List<int[]>();
            for (int i = 0; i < 20; i++)
            {
               commits.Add(_commits.GetNextCommit());
            }

            var template = commits.First();
            foreach (var commit in commits.Skip(1))
            {
                Assert.NotEqual(template, commit);
            }
        }


        public void SetFixture(GitCommitsFixture data)
        {
            _commits = GitCommits.Instance();
        }
    }

    public class GitCommitsFixture 
    {
    }
}
