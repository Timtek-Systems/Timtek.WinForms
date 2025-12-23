namespace Timtek.WinForms.MVVM;

/// <summary>
///     Example/documentation for using RelayCommand and AsyncRelayCommand in unit tests.
///     
///     The key point is that commands no longer depend directly on WinForms assemblies.
///     They use IUiThreadDispatcher, which is obtained from UiThreadDispatcherContext.Current.
///     
///     In a WinForms application:
///     - Commands are created on the UI thread
///     - UiThreadDispatcherContext.Current returns a UiThreadHelper
///     - Event notifications are marshalled to the UI thread
///     
///     In unit tests:
///     - Set the dispatcher context to use CurrentThreadDispatcher before creating ViewModels
///     - Commands can be created and tested without any WinForms context
///     - Event notifications execute on the current thread
///     
///     Example unit test setup:
///     
///     [TestFixture]
///     public class MyViewModelTests
///     {
///         [SetUp]
///         public void SetUp()
///         {
///             // Use CurrentThreadDispatcher for all commands created in this test
///             UiThreadDispatcherContext.SetDispatcher(new CurrentThreadDispatcher());
///         }
///         
///         [TearDown]
///         public void TearDown()
///         {
///             // Clear the override so other tests don't see it
///             UiThreadDispatcherContext.SetDispatcher(null);
///         }
///         
///         [Test]
///         public async Task MyCommand_ExecutesAsyncWork()
///         {
///             var viewModel = new MyViewModel();
///             
///             // Command can be created and used in tests without WinForms
///             viewModel.MyAsyncCommand.Execute(null);
///             
///             // Assertions...
///         }
///     }
/// </summary>
internal static class UiThreadDispatcherUsageExamples
{
}
