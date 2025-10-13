import axios, { type InternalAxiosRequestConfig, type AxiosResponse, type AxiosError } from 'axios';
import { toast } from 'react-toastify';
import { redirect } from 'react-router';
import { authService } from './authService';

const apiClient = axios.create();

apiClient.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => requestHandlers.handleConfig(config),
  (error) => requestHandlers.handleError(error)
);

apiClient.interceptors.response.use(
  (response: AxiosResponse) => responseHandlers.handleSuccess(response),
  (error: AxiosError) => responseHandlers.handleError(error)
);

const requestHandlers = {
  handleConfig(config: InternalAxiosRequestConfig): InternalAxiosRequestConfig {
    const token = authService.getToken();

    if (token !== null) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  handleError(error: AxiosError): Promise<never> {
    return Promise.reject(error);
  }
}

const responseHandlers = {
  handleSuccess(response: AxiosResponse): AxiosResponse {
    if (response.config.url?.endsWith('/login')) {
      toast.success('You successfully logged in!', {
        position: 'top-right',
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    }

    return response;
  },
  handleError(error: AxiosError): Promise<never> {
    if (error.response?.status === 401) {
      toast.error('You are not authorized. Please login again.', {
        position: 'top-right',
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
      redirect('/signin');
    }
    else {
      const errorMessage = (error.response?.data as any)?.message || error.message || 'An error occurred';

      toast.error(errorMessage, {
        position: 'top-right',
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
      });
    }

    return Promise.reject(error);
  }
}

export default apiClient;
