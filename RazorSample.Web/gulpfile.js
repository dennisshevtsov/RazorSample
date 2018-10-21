/// <binding BeforeBuild='css' />

var gulp = require('gulp');
var sass = require('gulp-sass');
var csso = require('gulp-csso');
var concat = require('gulp-concat');

gulp.task('sass', function () {
  return gulp.src('./Content/styles.scss')
             .pipe(sass().on('error', sass.logError))
             .pipe(gulp.dest('./Content/'));
});

gulp.task('css', ['sass'], function () {
  return gulp.src(['./Content/reset.css', './Content/styles.css'])
             .pipe(concat('styles.min.css'))
             .pipe(csso())
             .pipe(gulp.dest('./wwwroot/'));
});

gulp.task('default', function () {
  gulp.watch('./Content/*.scss', ['sass', 'css']);
});
