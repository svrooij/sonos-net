/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Pages/**/*.{razor,html,cshtml}',
    './Layout/**/*.{razor,html,cshtml}',
    './wwwroot/index.html'
  ],
  theme: {
    extend: {
      colors: {
        'sonos-primary': '#667eea',
        'sonos-secondary': '#764ba2',
      },
      fontFamily: {
        'sans': ['Segoe UI', 'system-ui', 'sans-serif'],
      },
      animation: {
        'pulse-slow': 'pulse 2s infinite',
        'spin-slow': 'spin 3s linear infinite',
      }
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/typography'),
  ],
}