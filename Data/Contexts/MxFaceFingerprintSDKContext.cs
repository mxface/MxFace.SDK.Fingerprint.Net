using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MxFace.Fingerprint.SDK.Sample.Net.Data.Contexts;

public class MxFaceFingerprintSDKContext : DbContext
{
    public MxFaceFingerprintSDKContext(DbContextOptions<MxFaceFingerprintSDKContext> options) : base(options)
    {
        System.Diagnostics.Debug.WriteLine("MxFaceFingerprintSDKContextas::ctor ->" + this.GetHashCode());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        MxFaceEntityFrameworkCoreHelpers.UseMxFaceFingerprintService(modelBuilder);
    }
}
