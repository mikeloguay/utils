using BenchmarkDotNet.Running;
using MyBenchmarks;

var summary = BenchmarkRunner.Run<Md5VsSha256>();