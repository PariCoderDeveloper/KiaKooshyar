using KiaKooshar.Application.DTOs.Identities.Captcha;
using KiaKooshar.Application.Features.Interfaces.Captcha;
using Microsoft.Extensions.Caching.Memory;
using SkiaSharp;
using System.Security.Cryptography;

public class CaptchaService : ICaptchaService
{
    private readonly IMemoryCache _cache;
    private const string CharPool = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
    private const int CodeLength = 5;
    private static readonly TimeSpan Expiration = TimeSpan.FromMinutes (2);

    public CaptchaService ( IMemoryCache cache )
    {
        _cache = cache;
    }

    public Task<CaptchaResultDto> GenerateAsync ()
    {
        var code = GenerateRandomCode ();
        var captchaId = Guid.NewGuid ().ToString ("N");

        _cache.Set (GetCacheKey (captchaId), code, Expiration);

        var imageBytes = DrawCaptchaImage (code);

        return Task.FromResult (new CaptchaResultDto
        {
            CaptchaId = captchaId,
            ImageBase64 = Convert.ToBase64String (imageBytes)
        });
    }

    public Task<bool> ValidateAsync ( string captchaId, string userInput )
    {
        var cacheKey = GetCacheKey (captchaId);

        if ( !_cache.TryGetValue (cacheKey, out string? correctCode) )
            return Task.FromResult (false);

        _cache.Remove (cacheKey);

        var isValid = string.Equals (
            correctCode,
            userInput?.Trim (),
            StringComparison.OrdinalIgnoreCase
            );

        return Task.FromResult (isValid);
    }

    private static string GetCacheKey ( string captchaId ) => $"captcha:{captchaId}";

    private static string GenerateRandomCode ()
    {
        var chars = new char[CodeLength];
        for ( int i = 0; i < CodeLength; i++ )
        {
            chars[i] = CharPool[RandomNumberGenerator.GetInt32 (CharPool.Length)];
        }
        return new string (chars);
    }

    private static byte[] DrawCaptchaImage ( string code )
    {
        const int width = 200;
        const int height = 70;

        using var surface = SKSurface.Create (new SKImageInfo (width, height));
        var canvas = surface.Canvas;

        canvas.Clear (SKColors.WhiteSmoke);

        var random = new Random ();

        using ( var noisePaint = new SKPaint { Color = SKColors.LightGray, StrokeWidth = 1 } )
        {
            for ( int i = 0; i < 6; i++ )
            {
                canvas.DrawLine (
                    random.Next (width), random.Next (height),
                    random.Next (width), random.Next (height),
                    noisePaint);
            }
        }

        using var font = new SKFont (SKTypeface.FromFamilyName ("Arial"), 32);
        float x = 15;

        foreach ( var ch in code )
        {
            using var paint = new SKPaint
            {
                Color = new SKColor (
                    (byte) random.Next (0, 100),
                    (byte) random.Next (0, 100),
                    (byte) random.Next (0, 100)),
                IsAntialias = true
            };

            canvas.Save ();
            canvas.Translate (x, height / 2f + 10);
            canvas.RotateDegrees (random.Next (-20, 20));
            canvas.DrawText (ch.ToString (), 0, 0, font, paint);
            canvas.Restore ();

            x += 32;
        }

        using var image = surface.Snapshot ();
        using var data = image.Encode (SKEncodedImageFormat.Png, 100);
        return data.ToArray ();
    }
}