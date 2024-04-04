package middleware

import (
	"net/http"
	"staff-web-app/logger"
	"staff-web-app/services"
)

func Auth(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		logger.Default.Debug("auth check")
		if !services.CheckSessionCookie(r) {
			authUrl := services.MakeOauth2AuthUrl()
			http.Redirect(w, r, authUrl, http.StatusSeeOther)
		}

		next.ServeHTTP(w, r)
	})
}
