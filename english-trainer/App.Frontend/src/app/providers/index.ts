import { appRouter } from './with-router';

export const withProviders = () => ({
  router: appRouter(),
});
