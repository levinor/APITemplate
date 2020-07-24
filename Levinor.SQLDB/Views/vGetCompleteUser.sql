CREATE VIEW [Common].[vGetCompleteUser]
	AS SELECT u.id as UserID, u.Name as UserName, u.Surename as UserSurename, u.Email as UserEmail , r.Name
	FROM Common.Users as u  INNER JOIN
	Common.Roles as r on u.RoleId = r.Id
