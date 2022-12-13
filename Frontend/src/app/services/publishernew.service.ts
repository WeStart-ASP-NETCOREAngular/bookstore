import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IPublisher } from '../Interfaces/IPublisher';

@Injectable({
  providedIn: 'root',
})
export class PublishernewService {
  baseURL = environment.baseURL + '/api/publisher';

  constructor(private httpClient: HttpClient) {}

  addPublisher(Name: string, Image: File) {
    let formData = new FormData();
    formData.append('Name', Name);
    formData.append('Image', Image);
    return this.httpClient.post<IPublisher>(this.baseURL, formData, {
      reportProgress: true,
      observe: 'events',
    });
  }
}
