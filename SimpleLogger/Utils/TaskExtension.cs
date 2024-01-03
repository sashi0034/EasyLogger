#nullable enable

using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleLogger.Utils;

public static class TaskExtension
{
    public static void Forget(
        this Task task,
        Action<Exception?>? errorHandler = null)
    {
        task.ContinueWith(t =>
            {
                if (t.IsFaulted && errorHandler != null) errorHandler(t.Exception);
            },
            TaskContinuationOptions.OnlyOnFaulted);
    }

    public static async Task RunErrorHandlerAsync(this Task task)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            if (e is OperationCanceledException oc)
                Console.WriteLine($"{e.Message} [{oc.HResult}]");
            else
                Console.Error.WriteLine(e);
        }
    }

    public static void RunErrorHandler(this Task task)
    {
        task.RunErrorHandlerAsync().Forget();
    }

    public static CancellationToken LinkToken(this CancellationToken cancel, CancellationTokenSource otherCancel)
    {
        return LinkToken(cancel, otherCancel.Token);
    }

    public static CancellationToken LinkToken(this CancellationToken cancel, CancellationToken otherCancel)
    {
        return CancellationTokenSource.CreateLinkedTokenSource(cancel, otherCancel).Token;
    }
}