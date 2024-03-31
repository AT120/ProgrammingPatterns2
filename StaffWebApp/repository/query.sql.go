// Code generated by sqlc. DO NOT EDIT.
// versions:
//   sqlc v1.25.0
// source: query.sql

package repository

import (
	"context"

	"github.com/jackc/pgx/v5/pgtype"
)

const getThemeByUserId = `-- name: GetThemeByUserId :one
SELECT theme FROM preferences
    WHERE userId = $1
`

func (q *Queries) GetThemeByUserId(ctx context.Context, userid pgtype.UUID) (NullTheme, error) {
	row := q.db.QueryRow(ctx, getThemeByUserId, userid)
	var theme NullTheme
	err := row.Scan(&theme)
	return theme, err
}

const getTokenBySessionId = `-- name: GetTokenBySessionId :one
SELECT accessToken FROM sessions 
    WHERE sessionId = $1
`

func (q *Queries) GetTokenBySessionId(ctx context.Context, sessionid pgtype.UUID) (pgtype.Text, error) {
	row := q.db.QueryRow(ctx, getTokenBySessionId, sessionid)
	var accesstoken pgtype.Text
	err := row.Scan(&accesstoken)
	return accesstoken, err
}

const initUserPreferences = `-- name: InitUserPreferences :exec
INSERT INTO preferences (userId) VALUES ($1)
`

func (q *Queries) InitUserPreferences(ctx context.Context, userid pgtype.UUID) error {
	_, err := q.db.Exec(ctx, initUserPreferences, userid)
	return err
}

const insertSession = `-- name: InsertSession :exec
INSERT INTO sessions (sessionId, accessToken) VALUES($1, $2)
`

type InsertSessionParams struct {
	Sessionid   pgtype.UUID
	Accesstoken pgtype.Text
}

func (q *Queries) InsertSession(ctx context.Context, arg InsertSessionParams) error {
	_, err := q.db.Exec(ctx, insertSession, arg.Sessionid, arg.Accesstoken)
	return err
}

const removeSession = `-- name: RemoveSession :exec
DELETE FROM sessions WHERE sessionId = $1
`

func (q *Queries) RemoveSession(ctx context.Context, sessionid pgtype.UUID) error {
	_, err := q.db.Exec(ctx, removeSession, sessionid)
	return err
}

const updateUserPreferences = `-- name: UpdateUserPreferences :exec
UPDATE preferences SET theme = $2
    WHERE userId = $1
`

type UpdateUserPreferencesParams struct {
	Userid pgtype.UUID
	Theme  NullTheme
}

func (q *Queries) UpdateUserPreferences(ctx context.Context, arg UpdateUserPreferencesParams) error {
	_, err := q.db.Exec(ctx, updateUserPreferences, arg.Userid, arg.Theme)
	return err
}
