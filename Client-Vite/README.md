# Furniture System - Vite Frontend (Bootstrap Version)

This is a React + TypeScript + Vite frontend application that replicates the functionality of the Angular furniture system application using Bootstrap for styling.

## Features

- **Authentication**: Sign in and sign up functionality with JWT tokens
- **Furniture Management**: Create, read, update, and delete furniture items
- **User Roles**: Support for regular users and Super Admin roles
- **Bootstrap Styling**: Uses the same Bootstrap version as the original Angular app
- **Type Safety**: Full TypeScript support for better development experience

## Tech Stack

- **React 18** - UI library
- **TypeScript** - Type safety
- **Vite** - Build tool and dev server
- **React Router** - Client-side routing
- **Bootstrap 4.1.3** - CSS framework (same as original Angular app)
- **Axios** - HTTP client
- **JWT Decode** - JWT token handling

## Getting Started

### Prerequisites

- Node.js (version 16 or higher)
- npm or yarn

### Installation

1. Install dependencies:
```bash
npm install
```

2. Start the development server:
```bash
npm run dev
```

3. Open your browser and navigate to `http://localhost:5173`

### Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint

## Project Structure

```
src/
├── components/          # React components
│   ├── Navigation.tsx
│   ├── Home.tsx
│   ├── SignIn.tsx
│   ├── SignUp.tsx
│   ├── FurnitureAll.tsx
│   ├── FurnitureAll.module.css
│   ├── FurnitureUser.tsx
│   ├── FurnitureDetails.tsx
│   └── CreateEditFurniture.tsx
├── guards/              # Route guards
│   ├── AuthGuard.tsx
│   └── SuperAdminGuard.tsx
├── models/              # TypeScript interfaces
│   └── furniture.ts
├── services/            # API services
│   ├── authService.ts
│   ├── furnitureService.ts
│   └── jwtInterceptor.ts  # JWT token interceptor
├── config/              # Configuration files
│   ├── environment.ts
│   └── environment.prod.ts
├── assets/              # Static assets
│   └── bootstrap.min.css
├── App.tsx              # Main app component
└── main.tsx             # App entry point
```

## API Integration

The application connects to the same backend API as the Angular version:
- Base URL: `http://localhost:5000`
- Authentication endpoints: `/auth/login`, `/auth/register`
- Furniture endpoints: `/furniture/*`

### JWT Token Management

The application includes a JWT interceptor (`jwtInterceptor.ts`) that automatically:
- Adds the `Authorization: Bearer <token>` header to all authenticated requests
- Handles token expiration by redirecting to the login page on 401 errors
- Manages token storage and retrieval from localStorage

## Features Comparison with Angular Version

This Vite application provides 1:1 functionality with the original Angular app:

- ✅ User authentication (sign in/sign up)
- ✅ Furniture listing (all furniture)
- ✅ User furniture management
- ✅ Furniture details view
- ✅ Create/edit furniture forms
- ✅ Role-based access control
- ✅ Responsive navigation
- ✅ Form validation
- ✅ Error handling
- ✅ **Identical Bootstrap styling**
- ✅ **Component-specific CSS styling**

## Bootstrap Styling

The application uses the exact same Bootstrap version (4.1.3) and CSS file as the original Angular app, ensuring:
- Identical visual appearance
- Same responsive behavior
- Consistent form styling
- Matching color scheme and typography

## Component-Specific Styling

Component-specific CSS from the Angular app has been transferred using CSS modules for scoped styling:
- **FurnitureAll.module.css**: Image sizing, card padding, button margins
- CSS modules provide the same scoped styling behavior as Angular's component CSS
- Styles are automatically scoped to prevent conflicts with other components

## Development

The application uses modern React patterns:
- Functional components with hooks
- TypeScript for type safety
- Bootstrap for styling (matching original Angular app)
- React Router for navigation
- Axios for API calls

## Building for Production

```bash
npm run build
```

The built files will be in the `dist` directory, ready for deployment.

## Migration from Tailwind

This version was migrated from Tailwind CSS to Bootstrap to match the original Angular app's styling exactly. All components have been rewritten to use Bootstrap classes and maintain the same visual appearance as the original application.