﻿using System;
using System.Collections.Generic;
using Nuke.Common;
using Nuke.Common.Tooling;

namespace DagAir.Components.Nuke.Tasks
{
    public static class DockerComposeTasks
    {
        private static string DockerComposePath =>
            ToolPathResolver.TryGetEnvironmentExecutable("DOCKERCOMPOSE_EXE") ??
            ToolPathResolver.GetPathExecutable("docker-compose");

        public static IReadOnlyCollection<Output> DockerCompose(
            string arguments,
            string workingDirectory = null,
            IReadOnlyDictionary<string, string> environmentVariables = null,
            int? timeout = null,
            bool? logOutput = null,
            bool? logInvocation = null,
            bool? logTimestamp = null,
            string logFile = null,
            Func<string, string> outputFilter = null)
        {
            var process = ProcessTasks.StartProcess(DockerComposePath,
                arguments, workingDirectory,
                environmentVariables,
                timeout,
                logOutput,
                logInvocation,
                logTimestamp,
                logFile,
                CustomLogger,
                outputFilter);

            process.AssertZeroExitCode();
            return process.Output;
        }

        static void CustomLogger(OutputType type, string output)
        {
            if (type == OutputType.Err
                && !output.EndsWith("failed")
                && (output.StartsWith("Creating"))
                || output.StartsWith("Starting")
                || output.StartsWith("Stopping")
                || output.StartsWith("Removing")
                || output.EndsWith("up-to-date")
            )
            {
                Logger.Info(output);
            }
            else
            {
                ProcessTasks.DefaultLogger(type, output);
            }
        }
    }
}