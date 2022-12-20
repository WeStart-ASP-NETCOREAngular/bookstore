import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { tap } from 'rxjs';
import { IPublisher } from 'src/app/Interfaces/IPublisher';
import { AuthService } from 'src/app/services/auth.service';
import { PublisherService } from 'src/app/services/publisher.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.css'],
})
export class PublisherComponent implements OnInit {
  constructor(
    private publisherService: PublisherService,
    private authService: AuthService
  ) {}

  CreatePublisherForm: FormGroup;
  uploadProgress: string;
  imageSrc: string | ArrayBuffer | null;

  page: number = 1;
  publishers: IPublisher[];

  ngOnInit(): void {
    this.CreatePublisherForm = new FormGroup({
      name: new FormControl('', Validators.required),
      file: new FormControl('', Validators.required),
      fileSource: new FormControl('', Validators.required),
    });
    this.publisherService.getPublishers().subscribe({
      next: (data) => {
        console.log(data);
        this.publishers = data;
      },
      error: (error: HttpErrorResponse) => {
        if (error.status == 403) {
          console.error('Error Forbidden', error.message);
        }
      },
    });
    // console.warn(this.authService.getFieldFromJWT('Email'));
  }

  get file() {
    return this.CreatePublisherForm.get('file')!;
  }

  onFileChange(event: Event) {
    const target = event.target as HTMLInputElement;
    const files = target.files!;
    if (files?.length > 0) {
      const file = files[0];
      console.log(file);

      const reader = new FileReader();
      reader.onload = (e) => (this.imageSrc = reader.result);
      reader.readAsDataURL(file);

      this.CreatePublisherForm.patchValue({
        fileSource: file,
      });
    } else this.imageSrc = null;
  }

  onSubmit() {
    // const file = this.
    const { name, fileSource } = this.CreatePublisherForm.value;
    console.log(this.CreatePublisherForm);
    this.publisherService
      .addPublisher(name, fileSource)
      .pipe(
        tap((event) => {
          if (event.type == HttpEventType.UploadProgress) {
            if (event.total != null)
              this.uploadProgress =
                Math.round(100 * (event.loaded / event.total)) + '%';
          }
        })
      )
      .subscribe({
        next: (data) => {
          if (data.type == HttpEventType.Response) {
            // console.log(data.body);
            this.imageSrc =
              environment.baseURL + '/Images/Thumbs/Med/' + data.body!.logo;
            this.CreatePublisherForm.reset();
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
