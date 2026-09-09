import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';

export interface CaptchaResult {
  captchaId: string;
  imageBase64: string;
}

@Injectable({ providedIn: 'root' })
export class CaptchaService {
  constructor(
    private api: ApiService
) {}

  public generate(): Observable<CaptchaResult> {
    return this.api.get<CaptchaResult>('captcha', 'generate');
  }
}