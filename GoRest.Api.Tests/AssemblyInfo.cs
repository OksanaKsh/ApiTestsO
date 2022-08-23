using NUnit.Framework;
//[assembly: Parallelizable(All)]
[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(5)]

