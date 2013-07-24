using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;

namespace RenderTheMatrix.Web
{
    public class GitCommits
    {
        private Repository _gitRepository;

        private const string RepositoryPath = @"C:\repo";
        private const int BufferSize = 1024;

        public GitCommits()
        {
            this._gitRepository = new Repository(RepositoryPath);
        }

        public IEnumerable<int> GetNextCommit()
        {
            var r = new Random();
            return Enumerable
                .Range(1, BufferSize)
                .Select(_ => r.Next());
        }
    }
}