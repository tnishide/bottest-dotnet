using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Tobii.Research;


namespace Bot_GazeTest.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private static Validity validity;
        private static float x, y, z;
        private static IEyeTracker eyeTracker;
        private static int message_count;

        public Task StartAsync(IDialogContext context)
        {
            message_count = 0;
            EyeTrackerCollection ec = EyeTrackingOperations.FindAllEyeTrackers(); // もしくはGetEyeTracker で URI指定できるらしい
            eyeTracker = null;
            int i = ec.Count;
            //Console.WriteLine(
            System.Diagnostics.Debug.WriteLine("FindAllEyeTrackers()... {0} devices found.", i.ToString());
            //label1.Text = i.ToString();
            if (i > 0)
            {
                eyeTracker = ec[0];
            }

            //label2.Text = eyeTracker.Address.ToString();
            //label2.Text = label2.Text + ", " + eyeTracker.DeviceName;
            // return our reply to the user
            if (eyeTracker != null)
            {
                ApplyLicense(eyeTracker, @"c:\tobii\license\license_key_00401181_-_Kyoto_University_IS404-100106332951");                
                GazeData(eyeTracker);
            }
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            System.Diagnostics.Debug.WriteLine("MessageReceivedAsync is called");
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            if (activity.Type == ActivityTypes.Message)
            {
                System.Diagnostics.Debug.WriteLine("ActivityTypes.Message");
                message_count++;
                //Console.WriteLine(
                System.Diagnostics.Debug.WriteLine("message count={0}.", message_count);
                await context.PostAsync($"MessageReceivedAsync is called : count={message_count}.");
                if (message_count == 1 && eyeTracker != null)
                {
                    await context.PostAsync($"{eyeTracker.Address.ToString()} is found.");
                }

                // return our reply to the user
                await context.PostAsync($"You sent {activity.Text} which was {length} characters");
                await context.PostAsync($"Your EYE location is X={x}, Y={y}, Z={z} with validity={validity}.");
                //            System.Diagnostics.Debug.WriteLine(
                //            "Got gaze data with {0} left eye origin at point ({1}, {2}, {3}) in the user coordinate system.",
                //            validity, x, y, z);

                var reply = context.MakeMessage() as IEventActivity;
                reply.Type = "event";
                reply.Name = "changeBackground";
                reply.Value = activity.Text;
                await context.PostAsync((IMessageActivity)reply);
            }
            else// if (activity.Type == ActivityTypes.Event)
            {
                System.Diagnostics.Debug.WriteLine("ActivityTypes.Event");
                if (activity.Name == "buttonClicked")
                {
                    System.Diagnostics.Debug.WriteLine("buttonClicked");
                    await context.PostAsync($"Event received : button pressed");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("not buttonClicked");
                    await context.PostAsync($"Event received but cannot understand...");
                }
            }

            context.Wait(MessageReceivedAsync);
        }
        // <BeginExample>
        private static void GazeData(IEyeTracker eyeTracker)
        {
            // Start listening to gaze data.
            eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
            // Wait for some data to be received.
            //System.Threading.Thread.Sleep(2000);
            // Stop listening to gaze data.
            //eyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
        }
        private static void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
                
            validity = e.LeftEye.GazeOrigin.Validity;
            x = e.LeftEye.GazeOrigin.PositionInUserCoordinates.X;
            y = e.LeftEye.GazeOrigin.PositionInUserCoordinates.Y;
            z = e.LeftEye.GazeOrigin.PositionInUserCoordinates.Z;

            //Console.WriteLine(
//            System.Diagnostics.Debug.WriteLine(
//            "Got gaze data with {0} left eye origin at point ({1}, {2}, {3}) in the user coordinate system.",
//            validity, x, y, z);

            //                e.LeftEye.GazeOrigin.Validity,
//                e.LeftEye.GazeOrigin.PositionInUserCoordinates.X,
//                e.LeftEye.GazeOrigin.PositionInUserCoordinates.Y,
//                e.LeftEye.GazeOrigin.PositionInUserCoordinates.Z);

        }

        private static void ApplyLicense(IEyeTracker eyeTracker, string licensePath)
        {
            // Create a collection with the license.
            var licenseCollection = new LicenseCollection(
            new System.Collections.Generic.List<LicenseKey>
            {
            new LicenseKey(System.IO.File.ReadAllBytes(licensePath))
            });
            // See if we can apply the license.
            FailedLicenseCollection failedLicenses;
            if (eyeTracker.TryApplyLicenses(licenseCollection, out failedLicenses))
            {
                //Console.WriteLine(
                System.Diagnostics.Debug.WriteLine("Successfully applied license from {0} on eye tracker with serial number {1}.",
                licensePath, eyeTracker.SerialNumber);
            }
            else
            {
                //Console.WriteLine(
                System.Diagnostics.Debug.WriteLine("Failed to apply license from {0} on eye tracker with serial number {1}.\n" +
                "The validation result is {2}.",
                licensePath, eyeTracker.SerialNumber, failedLicenses[0].ValidationResult);
            }
        }

    }

}