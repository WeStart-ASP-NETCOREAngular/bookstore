import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPublisher } from '../Interfaces/IPublisher';

@Injectable({
  providedIn: 'root',
})
export class PublisherService {
  baseURL = environment.baseURL + '/api/publisher';
  constructor(private httpClient: HttpClient) {}

  addPublisher(Name: string, Image: File) {
    const formData = new FormData();
    formData.append('Name', Name);
    formData.append('Image', Image);
    return this.httpClient.post<IPublisher>(this.baseURL, formData, {
      reportProgress: true,
      observe: 'events',
    });
  }

  getPublishers() {
    return this.httpClient.get<IPublisher[]>(this.baseURL).pipe(
      map((data) => {
        data.forEach((item) => {
          item.logo = environment.baseURL + '/Images/Thumbs/Med/' + item.logo;
          return item;
        });
        return data;
      })
    );
  }
}
