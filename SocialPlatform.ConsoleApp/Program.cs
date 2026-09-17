using ImageProcessing;
using SocialPlatform.Domain;
using SocialPlatform.Repository;
using SocialPlatform.Services;
using System;
using ImageProcessingLib;

namespace SocialPlatform.ConsoleApp;

internal class Program
{
    private static UserService _userService = null!;
    private static PostService _postService = null!;
    private static User? _currentUser = null;

    private static void Main(string[] args)
    {
        // 1. Initialize Repositories & Services
        var userRepository = new UserRepository();
        var postRepository = new PostRepository();

        _userService = new UserService(userRepository);
        _postService = new PostService(postRepository);

        // Continuous application loop
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("          ENTERPRISE SOCIAL PLATFORM              ");
            Console.WriteLine("==================================================");

            if (_currentUser == null)
            {
                Console.WriteLine(" Status: Not Logged In\n");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        ShowError("Invalid selection. Press Enter to retry.");
                        break;
                }
            }
            else
            {
                Console.WriteLine($" Status: Logged in as [{_currentUser.UserName}] (Age: {_currentUser.Age})\n");
                Console.WriteLine("1. Create Text Post");
                Console.WriteLine("2. Create Image Post");
                Console.WriteLine("3. View Global Feed");
                Console.WriteLine("4. Like a Post");
                Console.WriteLine("5. Comment on a Post");
                Console.WriteLine("6. Logout");
                Console.WriteLine("0. Exit Application");
                Console.Write("\nSelect an option: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateTextPost();
                        break;
                    case "2":
                        CreateImagePost();
                        break;
                    case "3":
                        ViewFeed();
                        break;
                    case "4":
                        LikePost();
                        break;
                    case "5":
                        CommentOnPost();
                        break;
                    case "6":
                        _currentUser = null;
                        ShowSuccess("Logged out successfully.");
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        ShowError("Invalid selection. Press Enter to retry.");
                        break;
                }
            }
        }

        Console.WriteLine("\nThank you for using the platform. Goodbye!");
    }

    // --- Authentication Actions ---

    private static void Register()
    {
        Console.Clear();
        Console.WriteLine("--- USER REGISTRATION ---");

        Console.Write("Enter Username: ");
        string? username = Console.ReadLine();

        Console.Write("Enter Birth Date (YYYY-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
        {
            ShowError("Invalid date format.");
            return;
        }

        Console.Write("Enter Password (min 8 chars): ");
        string? password = Console.ReadLine();

        // 1. Declare and read profilePic from the console
        Console.Write("Enter Profile Picture URL (press Enter to skip): ");
        string? profilePic = Console.ReadLine();

        try
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Fields cannot be empty.");
                return;
            }

            // 2. Now profilePic exists and can be passed into RegisterUser
            var registeredUser = _userService.RegisterUser(username, dob, password, profilePic ?? "https://example.com/default-avatar.png");
            ShowSuccess($"User '{registeredUser.UserName}' registered successfully! UserID: {registeredUser.UserId}");
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private static void Login()
    {
        Console.Clear();
        Console.WriteLine("--- USER LOGIN ---");

        Console.Write("Enter UserID (Guid): ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid userId))
        {
            ShowError("Invalid Guid format.");
            return;
        }

        var user = _userService.GetUser(userId);
        if (user == null)
        {
            ShowError("User not found!");
            return;
        }

        _currentUser = user;
        ShowSuccess($"Welcome back, {_currentUser.UserName}!");
    }

    // --- Content Actions ---

    private static void CreateTextPost()
    {
        Console.Clear();
        Console.WriteLine("--- CREATE TEXT POST ---");

        Console.Write("Enter post content: ");
        string? content = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(content))
        {
            ShowError("Post content cannot be empty.");
            return;
        }

        var post = _postService.CreateTextPost(_currentUser!.UserId, content);
        ShowSuccess($"Text Post created! Post ID: {post.ContentId}");
    }

    private static void CreateImagePost()
    {
        Console.Clear();
        Console.WriteLine("--- CREATE IMAGE POST ---");

        Console.Write("Enter post content/caption: ");
        string? content = Console.ReadLine();

        Console.Write("Enter Local Image File Path (\"C:\\Users\\tugsu\\Downloads\\wallpaperflare.com_wallpaper(1).jpg\"): ");
        string? inputPath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(inputPath))
        {
            ShowError("Caption and File Path cannot be empty.");
            return;
        }

        if (!File.Exists(inputPath))
        {
            ShowError($"File not found at: {inputPath}");
            return;
        }

        try
        {
            Console.WriteLine("\nProcessing image (Cropping and Resizing)...");

            // 1. Initialize ImageProcessor and Options
            var processor = new ImageProcessor();
            var options = new ImageProcessingOptions
            {
                RequiredWidthRatio = 1,
                RequiredHeightRatio = 1,
                MinWidth = 512,
                MaxWidth = 512
            };

            // 2. Open input file stream
            using FileStream inputStream = File.OpenRead(inputPath);

            // 3. Process image stream using your library
            using MemoryStream processedStream = processor.ProcessImage(inputStream, options);

            // 4. Save processed image stream to disk
            string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessedImages");
            Directory.CreateDirectory(outputDirectory);

            string outputFileName = $"post_{Guid.NewGuid()}.png";
            string outputPath = Path.Combine(outputDirectory, outputFileName);

            using (FileStream outputFileWriter = File.Create(outputPath))
            {
                processedStream.CopyTo(outputFileWriter);
            }

            // 5. Create post using the processed image path
            var post = _postService.CreateImagePost(_currentUser!.UserId, content, outputPath);

            ShowSuccess($"Image Post created successfully!\nProcessed File Saved To: {outputPath}\nPost ID: {post.ContentId}");
        }
        catch (Exception ex)
        {
            ShowError($"Image processing failed: {ex.Message}");
        }
    }

    private static void ViewFeed()
    {
        Console.Clear();
        Console.WriteLine("==================================================");
        Console.WriteLine("                  GLOBAL FEED                     ");
        Console.WriteLine("==================================================");

        var posts = _postService.GetAllPosts();
        bool hasPosts = false;

        foreach (var post in posts)
        {
            hasPosts = true;
            Console.WriteLine($"\n[Post ID]: {post.ContentId}");
            Console.WriteLine($"[Author ID]: {post.AuthorId}");
            Console.WriteLine($"[Content]: {post.ContentText}");

            if (post is ImagePost imgPost)
            {
                Console.WriteLine($"[Image URL]: {imgPost.ImageURL}");
            }

            Console.WriteLine($"[Likes]: {post.LikeCount} | [Comments]: {post.HowManyComment} | [Shares]: {post.ShareCount}");
            Console.WriteLine(new string('-', 50));
        }

        if (!hasPosts)
        {
            Console.WriteLine("\nNo posts available yet.");
        }

        Console.WriteLine("\nPress Enter to return to menu...");
        Console.ReadLine();
    }

    private static void LikePost()
    {
        Console.Clear();
        Console.WriteLine("--- LIKE A POST ---");

        Console.Write("Enter Post ID to Like: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid postId))
        {
            ShowError("Invalid Post ID format.");
            return;
        }

        bool result = _postService.LikePost(postId, _currentUser!.UserId);
        if (result)
            ShowSuccess("Liked post successfully!");
        else
            ShowError("Post not found.");
    }

    private static void CommentOnPost()
    {
        Console.Clear();
        Console.WriteLine("--- COMMENT ON POST ---");

        Console.Write("Enter Post ID: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid postId))
        {
            ShowError("Invalid Post ID format.");
            return;
        }

        Console.Write("Enter Comment Text: ");
        string? text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text))
        {
            ShowError("Comment cannot be empty.");
            return;
        }

        bool result = _postService.CommentOnPost(postId, _currentUser!.UserId, text);
        if (result)
            ShowSuccess("Comment added successfully!");
        else
            ShowError("Post not found.");
    }

    // --- Console Helpers ---

    private static void ShowSuccess(string message)
    {
        Console.WriteLine($"\nSUCCESS: {message}");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    private static void ShowError(string message)
    {
        Console.WriteLine($"\nERROR: {message}");
        Console.WriteLine("Press Enter to try again...");
        Console.ReadLine();
    }
}