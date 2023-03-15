
namespace BillsApp.Infrastructure.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly DateTime _now = DateTime.Now;
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            ValidateEntries(eventData);

            //foreach (var entry in entryList)
            //{
            //    if (IsAuditableEntity)
            //    {
            //        //auditEntity = entryAuditableList.FirstOrDefault(x => x.Entity.);
            //    }
            //}

            //if (IsAuditableEntity)
            //{
            //    foreach (var entry in entryList)
            //    {
            //        if(entry.State == EntityState.Modified || entry.HasChangedOwnedEntities()) UpdateAuditable(entry);                    
            //    }
            //}

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            ValidateEntries(eventData);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void ValidateEntries(DbContextEventData eventData)
        {

            if (eventData.Context!.ChangeTracker.Entries().Any())
            {
                var entriesList = eventData.Context!.ChangeTracker.Entries().ToList();
                //foreach (var entry in entriesList)
                //{
                //    var index = entriesList.IndexOf(entry);
                //    var clonedEntry = entry.CurrentValues.Clone().EntityType.ClrType.GetInterfaces();

                //    foreach (var implementedInterface in clonedEntry)
                //    {
                //        switch (implementedInterface.Name)
                //        {
                //            case "IAuditable":
                //                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities()) UpdateEntities(eventData.Context, eventData.Context!.ChangeTracker.Entries<IAuditable>().ToList()[index]);
                //                break;
                //            case "ISoftDelete":
                //                if (entry.State == EntityState.Deleted || entry.HasChangedOwnedEntities())
                //                {
                //                    entry.State = EntityState.Modified;
                //                    UpdateEntities(eventData.Context, eventData.Context!.ChangeTracker.Entries<IAuditable>().ToList()[index]);
                //                    UpdateSoftdelete(eventData.Context!.ChangeTracker.Entries<ISoftDelete>().ToList()[index]);
                //                }
                //                break;
                //            case "IStatus":
                //                if (entry.State != EntityState.Added)
                //                {
                //                    entry.State = EntityState.Modified;
                //                    UpdateEntities(eventData.Context, eventData.Context!.ChangeTracker.Entries<IAuditable>().ToList()[index]);
                //                    UpdateStatus(eventData.Context!.ChangeTracker.Entries<IStatus>().ToList()[index]);
                //                }
                //                break;
                //        }
                //    }
                //}
            }
        }

        public void UpdateEntities(DbContext? context, EntityEntry<IAuditable> entry)
        {
            if (context == null) return;

            UpdateAuditable(entry);
        }

        private void UpdateAuditable(EntityEntry<IAuditable> entity)
        {
            entity.Entity.LastModifiedBy = "user_visanet";
            entity.Entity.LastModified = _now;
        }

        private void UpdateSoftdelete(EntityEntry<ISoftDelete> entry)
        {
            entry.Entity.IsDeleted = true;
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified || r.TargetEntry.State == EntityState.Deleted));
}
