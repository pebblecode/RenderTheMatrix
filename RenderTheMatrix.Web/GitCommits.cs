using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LibGit2Sharp;

namespace RenderTheMatrix.Web
{
    public sealed class GitCommits : IDisposable
    {
        private readonly Repository _gitRepository;

        private const string RepositoryPath = @"C:\repo";

        public const int BufferSize = 128;

        private bool _disposed;

        private static readonly Lazy<GitCommits> _instance = 
            new Lazy<GitCommits>(() => new GitCommits(), 
                LazyThreadSafetyMode.PublicationOnly);

        private readonly IEnumerator<Commit> _commits;

        private GitCommits()
        {
            this._gitRepository = new Repository(RepositoryPath);
            this._commits = _gitRepository.Commits.GetEnumerator();
            _commits.MoveNext();
        }

        public static GitCommits Instance()
        {
            return _instance.Value;
        }

        public int[] GetNextCommit()
        {
            var author = _commits.Current.Author.GetHashCode();
            var currentTree = _commits.Current.Tree;
            var offset = 0;
            
            var data = Enumerable
                .Range(0, BufferSize -1)
                .Select(_ =>
                            {
                                if (currentTree.ElementAtOrDefault(_ - offset) == null)
                                    offset = _;
                                var ele = currentTree.ElementAt(_ - offset);
                                return ele.Target.Sha.GetHashCode();
                            });

            var commitData = (new[] {author}).Concat(data).ToArray();

            return commitData;
        }

        public void Dispose()
        {
            if(_disposed)
                return;
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if(disposing)
                _gitRepository.Dispose();

            _disposed = true;
        }
    }
}