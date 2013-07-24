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

        private IEnumerator<Commit> _commits;

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
            IEnumerable<char> patch = new char[]{};
            while (!patch.Any())
            {
                if (!_commits.MoveNext())
                    _commits = _gitRepository.Commits.GetEnumerator();
                var next = _commits.Current;
                var nextTree = next.Tree;
                var diff = _gitRepository.Diff.Compare(currentTree, nextTree);
                patch = diff.Patch;
            }
            patch = patch.Reverse();

            var offset = 0;
            
            var data = Enumerable
                .Range(0, BufferSize -1)
                .Select(_ =>
                            {
                                if (patch.ElementAtOrDefault(_ - offset) == default(char))
                                    offset = _;
                                 return (int)(patch.ElementAt(_ - offset));
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