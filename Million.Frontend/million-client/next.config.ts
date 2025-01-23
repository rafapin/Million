import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  reactStrictMode: true,
  env: {
    API_URL: 'https://localhost:7187', // URL de tu API
  },
};

// Deshabilitar validación de certificados autofirmados
process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

export default nextConfig;

