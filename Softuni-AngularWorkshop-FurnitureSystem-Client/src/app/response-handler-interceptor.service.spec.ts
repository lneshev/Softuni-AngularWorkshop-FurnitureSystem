import { TestBed } from '@angular/core/testing';

import { ResponseHandlerInterceptorService } from './response-handler-interceptor.service';

describe('ResponseHandlerInterceptorService', () => {
  let service: ResponseHandlerInterceptorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResponseHandlerInterceptorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
